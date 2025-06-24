using Godot;
using System;

public enum CombatState {
	Idle,
	Moving,
	Chasing,
	Attacking,
	AttackMoving
}

public partial class CombatComponent : Node
{
	public Character CharacterOwner { get; set; }
	public Character Target { get; private set; }
	
	public CombatState State { get; private set; } = CombatState.Idle;
	
	public float AttackCooldown = 1.5f;
	public float attackTimer = 0f;
	
	public float DestinationTolerance = 100f;
	
	// boolean to check if the Attack Move command was issued
	public bool IsAttackMoving = false;
	
	public void Update(double delta) {
		if ((State == CombatState.Attacking || State == CombatState.Chasing) && (Target == null || !Target.IsAlive)) {
			ClearTarget();
			return;
		}
		float distance = 0f;
		if (Target != null) {
			distance = CharacterOwner.GlobalPosition.DistanceTo(Target.GlobalPosition);
		}
		
		switch (State) {
			case CombatState.Chasing:
				if (distance > CharacterOwner.AttackRange) {
					Vector2 destination = Target.GlobalPosition;
					if (CharacterOwner is PlayerCharacter player) {
						player.MoveTo(destination);
					} else if (CharacterOwner is EnemyCharacter enemy) {
						enemy.MoveTo(destination);
					}
				} else {
					ChangeCombatState(CombatState.Attacking);
					CharacterOwner.StopMoving();
				}
				break;
			
			case CombatState.Attacking:
				if (distance > CharacterOwner.AttackRange) {
					ChangeCombatState(CombatState.Chasing);
					break;
				} else {
					attackTimer -= (float)delta;
					
					if (attackTimer <= 0f) {
					PerformAttack();
					attackTimer = AttackCooldown;
					}
				}
				break;
				
				case CombatState.AttackMoving: 
					var distanceToDest = CharacterOwner.Position.DistanceTo(CharacterOwner.FinalDestination);
					if (IsAttackMoving && distanceToDest > DestinationTolerance) {
						CharacterOwner.MoveTo(CharacterOwner.FinalDestination);
						
						var nearestEnemy = FindNearestEnemyInRange();
						if (nearestEnemy != null) {
							SetTarget(nearestEnemy);
						}
					} else {
						IsAttackMoving = false;
						ChangeCombatState(CombatState.Idle);
					}
					break;
				
				
			case CombatState.Idle:
				break;
				
			case CombatState.Moving:
				break;
		}
	}
	
	public void SetTarget(Character target) {
		Target = target;
		if (target != null && target.IsAlive) {
			ChangeCombatState(CombatState.Chasing);
		} else {
			ChangeCombatState(CombatState.Idle);
		}
	}
	
	public void ClearTarget() {
		Target = null;
		if (IsAttackMoving) {
			ChangeCombatState(CombatState.AttackMoving);
		} else {
			ChangeCombatState(CombatState.Idle);
			CharacterOwner.StopMoving();
		}
	}
	
	private void PerformAttack() {
		if (Target == null || !Target.IsAlive) {
			ClearTarget();
			return;
		}
		CombatManager.DealDamage(CharacterOwner, Target);
	}
	
	private Character FindNearestEnemyInRange() {
		return CharacterOwner.AttackRangeDetector.GetNearestEnemy();
	}
	
	public void ChangeCombatState(CombatState newState) {
		// Do nothing if State doesn't change
		if (State == newState) {
			return;
		}
		
		State = newState;
		//GD.Print($"Combat State Changed To: {State}");
	}
}
