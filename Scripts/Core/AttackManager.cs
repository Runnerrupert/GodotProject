using Godot;
using System;
using System.Collections.Generic;

public partial class AttackManager : Node2D
{
	public static AttackManager Instance;
	
	public override void _Ready(){
		Instance = this;
	}
	
	public void CommandAttack(EnemyCharacter target) {
		foreach(var playerUnit in SelectionManager.Instance.GetSelectedPlayerUnits()) {
			if (playerUnit.CurrentTarget != target) {
				playerUnit.Combat.SetTarget(target);
			}
			GD.Print($"{playerUnit.CharacterName} is now targeting {target.CharacterName}!");
		}
	}
}
