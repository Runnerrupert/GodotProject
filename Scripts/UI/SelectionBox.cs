using Godot;
using System;

public partial class SelectionBox : Node2D
{
	private Vector2 _startWorldPos;
	private Vector2 _endWorldPos;
	private bool _selecting = false;
	private Rect2 _selectionRect;
	
	public override void _Input(InputEvent @event) {
		if (@event is InputEventMouseButton mouseEvent) {
			if (mouseEvent.ButtonIndex == MouseButton.Left) {
				if (mouseEvent.Pressed) {
					_startWorldPos = GetViewport().GetCanvasTransform().AffineInverse() * mouseEvent.Position;
					_selecting = true;
				} else if (!mouseEvent.Pressed && _selecting) {
					_selecting = false;
					if (_selectionRect.Size.Length() > 5) {
						SelectUnitsInRect(_selectionRect);
					} else {
						TrySelectSingleUnitAt(_startWorldPos);
					}
					// reset _selectionRect to properly gauge if the player is clicking or making another selection
					_selectionRect = new Rect2();
					QueueRedraw();
				} 
			}
		}
		
		if (@event is InputEventMouseMotion motionEvent && _selecting) {
			_endWorldPos = GetViewport().GetCanvasTransform().AffineInverse() * motionEvent.Position;
			_selectionRect = new Rect2(_startWorldPos, _endWorldPos - _startWorldPos).Abs();
			QueueRedraw();
		}
	}
	
	public override void _Draw() {
		if (_selecting) {
			DrawRect(_selectionRect, new Color(0, 1, 0, 0.25f), filled: true);
			DrawRect(_selectionRect, new Color(0, 1, 0), filled: false);
		}
	}
	
	private bool IsHitboxOverlapping(PlayerCharacter player, Rect2 area) {
		var hitbox = player.GetNode<Area2D>("Hitbox");
		var collision = hitbox.GetNode<CollisionShape2D>("CollisionShapeHitbox");
		if (collision.Shape is RectangleShape2D rectShape) {
			var topLeft = hitbox.GlobalPosition - (rectShape.Size * 0.5f);
			var rect = new Rect2(topLeft, rectShape.Size);
			return area.Intersects(rect);
		}
		return false;
	}
	
	private void SelectUnitsInRect(Rect2 selectionRect) {
		var units = GetTree().GetNodesInGroup("player_units");
		foreach(Node unit in units) {
			if (unit is PlayerCharacter player) {
				bool isSelected = IsHitboxOverlapping(player, selectionRect);
				player.SetSelected(isSelected);
			}
		}
	}
	
	private bool TrySelectSingleUnitAt(Vector2 position) {
		var units = GetTree().GetNodesInGroup("player_units");
		var clickRect = new Rect2(position - Vector2.One, Vector2.One * 2);
		
		// Boolean for selecting only 1 unit at a time, even if there is overlap
		bool unitSelected = false;
		
		foreach(Node unit in units) {
			if (unit is PlayerCharacter player) {
				bool isSelected = IsHitboxOverlapping(player, clickRect);
				
				if (!unitSelected && isSelected) {
					player.SetSelected(true);
					unitSelected = true;
				} else {
					if (MovementManager.Instance.currentInputMode != InputMode.AttackMovePending) {
						player.SetSelected(false);
					}
				}
			}
		}
		
		if (MovementManager.Instance.currentInputMode == InputMode.AttackMovePending) {
			MovementManager.Instance.currentInputMode = InputMode.Normal;
		}
		
		return unitSelected;
	}
	
	private void DeselectAllUnits() {
		foreach (Node unitNode in GetTree().GetNodesInGroup("player_units")) {
			if (unitNode is PlayerCharacter unit) {
				unit.SetSelected(false);
				SelectionManager.Instance.UpdateSelection(unit, false);
			}
		}
	}
}
