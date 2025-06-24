using Godot;
using System;
using System.Collections.Generic;

public partial class SelectionManager : Node2D {
	
	public static SelectionManager Instance;
	private Dictionary<Node2D, bool> _selectedUnits = new();
	
	public override void _Ready() {
		Instance = this;
	}
	
	public void UpdateSelection(Node2D unit, bool selected) {
		if (selected) {
			_selectedUnits[unit] = true;
			if (unit.GetNodeOrNull<SelectionCircle>("SelectionCircle") == null) {
				var circle = new SelectionCircle();
				var vc = unit.GetNodeOrNull<VisualComponent>("VisualComponent");
				if (vc != null && vc.Sprite != null) {
					circle.AttachTo(vc.Sprite);
				} else {
					GD.Print("Cannot find Sprite via VisualComponent");
				}
				unit.AddChild(circle);
				circle.Name = "SelectionCircle";
			}
		} else {
			_selectedUnits.Remove(unit);

			var existingCircle = unit.GetNodeOrNull<SelectionCircle>("SelectionCircle");
			existingCircle?.QueueFree();
		}
		
		MovementManager.Instance.UpdateSelectedUnits(GetSelectedUnits());
	}
	
	// Code for Deselecting a Unit after Death
	public void Deselect(Node2D unit) {
		if (_selectedUnits.ContainsKey(unit)) {
			_selectedUnits.Remove(unit);
			
			var existingCircle = unit.GetNodeOrNull<SelectionCircle>("SelectionCircle");
			existingCircle?.QueueFree();
			
			MovementManager.Instance.UpdateSelectedUnits(GetSelectedUnits());
		}
	}
	
	
	// Helper Function used by MovementManager.cs
	public List<Node2D> GetSelectedUnits() {
		return new List<Node2D>(_selectedUnits.Keys);
	}
	
	public List<PlayerCharacter> GetSelectedPlayerUnits() {
		var playerUnits = new List<PlayerCharacter>();
		foreach (var unit in _selectedUnits.Keys) {
			if (unit is PlayerCharacter player) {
				playerUnits.Add(player);
			}
		}
		return playerUnits;
	}
}
