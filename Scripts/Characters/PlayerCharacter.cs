using Godot;
using System;

public partial class PlayerCharacter : Character
{
	public bool IsSelected = false;
	
	public override void _Ready() {
		base._Ready();
		hitbox = GetNode<Area2D>("Hitbox");
		hitbox.Connect("input_event", new Callable(this, nameof(OnHitboxInput)));
	}
	
	public override void _Process(double delta) {
		base._Process(delta);
	}
	
	public void SetSelected(bool selected) {
		if (IsSelected == selected) {
			return;
		}
		
		IsSelected = selected;
		SelectionManager.Instance.UpdateSelection(this, selected);
	}
	
	public void OnHitboxInput(Node viewport, InputEvent @event, int shapeIdx) {
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			if (mouseEvent.ButtonIndex == MouseButton.Left) {
				GD.Print($"{GetType().Name} hitbox left clicked");
				SetSelected(true);
			} 
		} 
	}
	
	public override void Die() {
		base.Die();
		
		SelectionManager.Instance.Deselect(this);
	}
}
