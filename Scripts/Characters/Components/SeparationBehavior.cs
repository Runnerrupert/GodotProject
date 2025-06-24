using Godot;
using System;
using System.Collections.Generic;

public partial class SeparationBehavior : Node
{
	public Character CharacterOwner { get; set; }
	public List<Character> OverlappingCharacters = new();
	
	
	public override void _Process(double delta) {
		ApplySeparation((float)delta);
	}
	
	
	public void OnAreaEntered(Area2D area) {
		if (area.Owner is Character other && other != CharacterOwner) {
			OverlappingCharacters.Add(other);
		}
	}
	
	public void OnAreaExited(Area2D area) {
		if (area.Owner is Character other && other != CharacterOwner) {
			OverlappingCharacters.Remove(other);
		}
	}
	
	public void ApplySeparation(float delta) {
		const float SeparationForce = 300f;
		const float IdleResistanceMultiplier = 2.5f;
		const float MoveYieldMultiplier = 0.4f;
		const float MaxPushPerFrame = 10f;
		
		Vector2 totalPush = Vector2.Zero;
		
		foreach (var other in OverlappingCharacters) {
			
			if (other == CharacterOwner || !other.IsAlive) {
				continue;
			}
			
			Vector2 offset = CharacterOwner.GlobalPosition - other.GlobalPosition;
			float distance = offset.Length();
			
			if (distance == 0) {
				continue;
			}
			
			Vector2 pushDir = offset.Normalized();
			float force = SeparationForce / distance;
			
			
			float selfMultiplier = CharacterOwner.Combat.State == CombatState.Idle ? IdleResistanceMultiplier : MoveYieldMultiplier;
			float otherMultiplier = other.Combat.State == CombatState.Idle ? IdleResistanceMultiplier : MoveYieldMultiplier;
			
			Vector2 selfPush = pushDir * force * otherMultiplier * (float)delta;
			
			totalPush += selfPush;
		}
		
		if (totalPush.Length() > MaxPushPerFrame) {
			totalPush = totalPush.Normalized() * MaxPushPerFrame;
		}
		
		CharacterOwner.GlobalPosition += totalPush;
	}
}
