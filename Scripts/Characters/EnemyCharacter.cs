using Godot;
using System;

public partial class EnemyCharacter : Character
{	
	public override void _Ready() {
		base._Ready();
		hitbox = GetNode<Area2D>("Hitbox");
		hitbox.Connect("input_event", new Callable(this, nameof(OnHitboxInput)));
		
		var enemyAI = new EnemyAI();
		AddChild(enemyAI);
	}
	
	public override void _Process(double delta) {
		base._Process(delta);
	}
	
	public void OnHitboxInput(Node viewport, InputEvent @event, int shapeIdx) {
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed) {
			if (mouseEvent.ButtonIndex == MouseButton.Right) {
				var selectedUnits = SelectionManager.Instance.GetSelectedPlayerUnits();
				if (selectedUnits.Count > 0) {
					AttackManager.Instance.CommandAttack(this);
				}
			} else if (mouseEvent.ButtonIndex == MouseButton.Left && MovementManager.Instance.currentInputMode == InputMode.AttackMovePending) {
				var selectedUnits = SelectionManager.Instance.GetSelectedPlayerUnits();
				if (selectedUnits.Count > 0) {
					AttackManager.Instance.CommandAttack(this);
				}
			}
		}
	}
}
