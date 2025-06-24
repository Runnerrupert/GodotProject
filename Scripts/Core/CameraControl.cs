using Godot;
using System;

public partial class CameraControl : Camera2D
{
	// Camera Pan
	public float moveSpeed = 500f;
	public float edgeScrollDistance = 50f;
	public float edgeScrollSpeed = 200f;
	
	// Zoom
	public float zoomStep = 0.02f;
	public Vector2 minZoom = new Vector2(0.5f, 0.5f);
	public Vector2 maxZoom = new Vector2(3f, 3f);
	
	// Zoom Tracking
	private Vector2? zoomOriginWorldPos = null;
	private Vector2 lastMousePosForZoom;
	private const float mouseMoveThreshold = 5f;
	
	// Camera Middle Mouse Movement
	private bool isMiddleMouseDragging = false;
	private Vector2 lastMousePosition;
	
	// Unit Focus
	private int focusUnitIndex = 0;
	private float cycleCooldown = 0.2f;
	private float timeSinceLastCycle = 0f;
	private bool isFocusToggled = false;
	
	public override void _Ready() {
		GD.Print("Camera Initialized");
	}
	
	public override void _Process(double delta) {
		Vector2 cameraMovement = Vector2.Zero;
		
		if (Input.IsActionPressed("ui_up")) cameraMovement.Y -= 2;
		if (Input.IsActionPressed("ui_down")) cameraMovement.Y += 2;
		if (Input.IsActionPressed("ui_left")) cameraMovement.X -= 2;
		if (Input.IsActionPressed("ui_right")) cameraMovement.X += 2;
		
		Vector2 zoomFactor = Zoom;
		cameraMovement = cameraMovement.Normalized() * moveSpeed * zoomFactor * (float)delta;
		Position += cameraMovement;
		
		Vector2 mousePos = GetViewport().GetMousePosition();
		Vector2 screenSize = GetViewport().GetVisibleRect().Size;
		
		if (!isMiddleMouseDragging) {
			if (mousePos.X < edgeScrollDistance) {
			Position += new Vector2(-edgeScrollSpeed / Zoom.X * (float)delta, 0);
			}
			if (mousePos.X > screenSize.X - edgeScrollDistance) {
				Position += new Vector2(edgeScrollSpeed / Zoom.X * (float)delta, 0);
			}
			if (mousePos.Y < edgeScrollDistance) {
				Position += new Vector2(0, -edgeScrollSpeed / Zoom.Y * (float)delta);
			}
			if (mousePos.Y > screenSize.Y - edgeScrollDistance) {
				Position += new Vector2(0, edgeScrollSpeed / Zoom.Y * (float)delta);
			}
		}
		
		timeSinceLastCycle += (float)delta;
		
		var selectedUnits = SelectionManager.Instance?.GetSelectedPlayerUnits();
		if (isFocusToggled && (selectedUnits == null || selectedUnits.Count == 0)) {
			isFocusToggled = false;
			GD.Print($"Focus toggled: {isFocusToggled}");
		}
			
		if((isFocusToggled || Input.IsActionPressed("camera_focus_hold"))) {
			if (focusUnitIndex >= selectedUnits.Count) {
				focusUnitIndex = 0;
			}
			Vector2 focusPos = selectedUnits[focusUnitIndex].GlobalPosition;
			Vector2 inverseZoom = new Vector2(1f / Zoom.X, 1f / Zoom.Y);
			Vector2 offsetPos = focusPos - (GetViewport().GetVisibleRect().Size / 2f) * inverseZoom;
			Position = Position.Lerp(offsetPos, 5f * (float)delta);
		}
		if (Input.IsActionJustPressed("camera_focus_toggle")) {
			isFocusToggled = !isFocusToggled;
			GD.Print($"Focus toggled: {isFocusToggled}");
		}
	}
	
	public override void _Input(InputEvent @event) {
		if (@event is InputEventMouseButton mouseButton) {
			if (mouseButton.ButtonIndex == MouseButton.Middle) {
				if (mouseButton.Pressed) {
					isMiddleMouseDragging = true;
					lastMousePosition = mouseButton.Position;
					GetViewport().SetInputAsHandled();
				} else {
					isMiddleMouseDragging = false;
				}
			}
			
			if (mouseButton.ButtonIndex == MouseButton.WheelUp || mouseButton.ButtonIndex == MouseButton.WheelDown) {
				Vector2 currentMousePos = mouseButton.Position;
				
				if (!zoomOriginWorldPos.HasValue || currentMousePos.DistanceTo(lastMousePosForZoom) > mouseMoveThreshold) {
					zoomOriginWorldPos = GetGlobalMousePosition();
				}
				
				float zoomMultiplier = mouseButton.ButtonIndex == MouseButton.WheelUp ? 1f + zoomStep : 1f - zoomStep;
				
				ZoomTowardWorldPoint(zoomMultiplier, zoomOriginWorldPos.Value);
				
				lastMousePosForZoom = currentMousePos;
				
				GetViewport().SetInputAsHandled();
			} 
		}
		
		if (@event is InputEventMouseMotion mouseMotion) {
			if (zoomOriginWorldPos.HasValue) {
				if (mouseMotion.Position.DistanceTo(lastMousePosForZoom) > mouseMoveThreshold) {
					zoomOriginWorldPos = GetGlobalMousePosition();
					lastMousePosForZoom = mouseMotion.Position;
				}
			}
			if (isMiddleMouseDragging) {
				Vector2 delta = (mouseMotion.Position - lastMousePosition) / Zoom;
				Position -= delta;
				lastMousePosition = mouseMotion.Position;
				GetViewport().SetInputAsHandled();
			}
		}
		
		if (@event is InputEventKey keyEvent && keyEvent.Pressed && timeSinceLastCycle > cycleCooldown) {
			var selected = SelectionManager.Instance?.GetSelectedPlayerUnits();
			if (selected == null || selected.Count <= 1) {
				return;
			}
			
			if (Input.IsActionJustPressed("camera_cycle_prev")) {
				GD.Print("Shifted to Prev Selection");
				focusUnitIndex--;
				if (focusUnitIndex < 0) {
					focusUnitIndex = selected.Count - 1;
				}
				
				timeSinceLastCycle = 0f; 
			} else if (Input.IsActionJustPressed("camera_cycle_next")) {
				GD.Print("Shifted to Next Selection");
				focusUnitIndex = (focusUnitIndex + 1) % selected.Count;
				timeSinceLastCycle = 0f;
			} 
		}
	}
	
	private void ZoomTowardWorldPoint(float zoomMultiplier, Vector2 zoomPointWorld) {
		Vector2 newZoom = Zoom * zoomMultiplier;
		newZoom.X = Mathf.Clamp(newZoom.X, minZoom.X, maxZoom.X);
		newZoom.Y = Mathf.Clamp(newZoom.Y, minZoom.Y, maxZoom.Y);

		// Get the position of zoomPointWorld before zoom
		Vector2 beforeZoomScreenPos = WorldToScreen(zoomPointWorld);

		// Apply zoom
		Zoom = newZoom;

		// Get position of zoomPointWorld after zoom
		Vector2 afterZoomScreenPos = WorldToScreen(zoomPointWorld);

		// Calculate difference and adjust camera position so zoomPointWorld stays under the mouse cursor
		Vector2 screenDelta = afterZoomScreenPos - beforeZoomScreenPos;
		Position += screenDelta / newZoom;
	}
	
	private Vector2 WorldToScreen(Vector2 worldPos) {
		return (worldPos - Position) * Zoom + GetViewport().GetVisibleRect().Size / 2f;
	}
}
