using Godot;
using System; 
using System.Collections.Generic;

public partial class AttackRangeDetector : Area2D
{
	public Character CharacterOwner { get; set; }
	
	private readonly List<Character> enemiesInRange = new();
	
	public override void _Ready() {
		GD.Print("AttackRangeDetector Initializing...");
		Connect("area_entered", new Callable(this, nameof(OnAreaEntered)));
		Connect("area_exited", new Callable(this, nameof(OnAreaExited)));
	}
	
	private void OnAreaEntered(Area2D area) {
		if (area.Owner is Character character) {
			if (CharacterOwner.IsEnemy(character)) {
				if (!enemiesInRange.Contains(character)) {
					enemiesInRange.Add(character);
				}
			}
		}
	}
	
	private void OnAreaExited(Area2D area) {
		if (area.Owner is Character character) {
			if (enemiesInRange.Contains(character)) {
				enemiesInRange.Remove(character);
			}
		}
	}
	
	public Character GetNearestEnemy() {
		Character nearestEnemy = null;
		float nearestDistance = float.MaxValue;
		
		Vector2 ownerPos = GlobalPosition;
		
		foreach (var enemy in enemiesInRange) {
			if (enemy == null || !enemy.IsAlive) {
				continue;
			}
			
			float dist = ownerPos.DistanceTo(enemy.GlobalPosition);
			if (dist < nearestDistance) {
				nearestDistance = dist;
				nearestEnemy = enemy;
			}
		}
		return nearestEnemy;
	}
	
	public bool HasEnemiesInRange() => enemiesInRange.Count > 0;
}
