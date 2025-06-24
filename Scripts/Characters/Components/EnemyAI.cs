using Godot;
using System;

public partial class EnemyAI : Node
{
	[Export] public float DetectionRadius = 300f;
	[Export] public float PatrolInterval = 3f;
	
	private EnemyCharacter enemy;
	private float patrolTimer = 0f;
	private Vector2 patrolCenter;
	private Random random = new Random();
	
	public override void _Ready() {
		enemy = GetParent<EnemyCharacter>();
		patrolCenter = enemy.GlobalPosition;
	}
	
	public override void _Process(double delta) {
		if (!enemy.IsAlive) return;

		patrolTimer += (float)delta;

		if (enemy.Combat.State == CombatState.Idle || enemy.Combat.State == CombatState.Moving)
		{
			Character nearest = FindNearestVisiblePlayer();
			if (nearest != null)
			{
				enemy.Combat.SetTarget(nearest);
				return;
			}

			if (patrolTimer >= PatrolInterval)
			{
				patrolTimer = 0f;
				Vector2 patrolTarget = patrolCenter + new Vector2(
					(float)(random.NextDouble() * 200 - 100),
					(float)(random.NextDouble() * 200 - 100)
				);
				enemy.MoveTo(patrolTarget);
				enemy.SetFinalDestination(patrolTarget);
				enemy.Combat.ChangeCombatState(CombatState.Moving);
			}
		}
	}
	
	private Character FindNearestVisiblePlayer() {
		float closestDist = DetectionRadius;
		Character closestPlayer = null;

		foreach (Character other in GetTree().GetNodesInGroup("player_units"))
		{
			if (other == null || !other.IsAlive) continue;

			float dist = enemy.GlobalPosition.DistanceTo(other.GlobalPosition);
			if (dist < closestDist)
			{
				closestDist = dist;
				closestPlayer = other;
			}
		}

		return closestPlayer;
	}
	
}
