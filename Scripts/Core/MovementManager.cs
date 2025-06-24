using Godot;
using System;
using System.Collections.Generic;

public enum InputMode {
	Normal,
	AttackMovePending
}

public partial class MovementManager : Node2D
{
	public static MovementManager Instance;
	
	private List<PlayerCharacter> selectedUnits = new();
	public InputMode currentInputMode = InputMode.Normal;
	
	public override void _Ready() {
		Instance = this;
	}
	
	public void UpdateSelectedUnits(List<Node2D> units) {
		selectedUnits.Clear();
		foreach (var unit in units) {
			if (unit is PlayerCharacter player) {
				selectedUnits.Add(player);
			}
		}
	}
	
	public Vector2 RandomizeDestinationOffset() {
		var offset = new Vector2 (
			(float)GD.RandRange(0,0),	// Default is -12, 12
			(float)GD.RandRange(0,0)		// Default is -12, 12
		);
		return offset;
	}
	
	public override void _Input(InputEvent @event) {
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo) {
			if (keyEvent.Keycode == Key.A) {
				var selectedUnits = SelectionManager.Instance.GetSelectedPlayerUnits();
				if (selectedUnits.Count > 0) {
					currentInputMode = InputMode.AttackMovePending;
					GD.Print("Attack Move mode activated");
					return;
				}
			}
		}
		
		
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			Vector2 worldPos = GetViewport().GetCanvasTransform().AffineInverse() * mouseEvent.Position;
			
			
			
			if (mouseEvent.ButtonIndex == MouseButton.Left && currentInputMode == InputMode.AttackMovePending) {
				foreach (var unit in selectedUnits) {
					
					// Set up randomized offsets for final destination placement
					var offset = RandomizeDestinationOffset();
					
					unit.Combat.IsAttackMoving = true;
					unit.SetFinalDestination(worldPos + offset);
					unit.MoveTo(worldPos);
					unit.Combat.ChangeCombatState(CombatState.AttackMoving);
				}
				return;
			}
			
			if (mouseEvent.ButtonIndex == MouseButton.Right) {
				foreach(var unit in selectedUnits) {
					
					// Set up randomized offsets for destination placement
					var offset = RandomizeDestinationOffset();
					
					unit.Combat.ClearTarget();
					unit.Combat.IsAttackMoving = false;
					unit.MoveTo(worldPos + offset);
					unit.Combat.ChangeCombatState(CombatState.Moving);
				}
				return;
			}
		} 
	}
}
