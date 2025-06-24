using Godot;
using System;

public partial class SelectionCircle : Node2D
{
	private Sprite2D _sprite;

	public override void _Ready() {
		ZIndex = -1;
	}

	public override void _Draw() {
		if (_sprite == null || _sprite.Texture == null) {
			return; 
		}

		float spriteHeight = _sprite.Texture.GetHeight() * _sprite.Scale.Y;
		float spriteWidth = _sprite.Texture.GetWidth() * _sprite.Scale.X;

		Vector2 offset = new Vector2(0, spriteHeight / 2.25f);
		Vector2 localPos = offset;

		float radiusX = spriteWidth / 1.5f;
		float radiusY = spriteHeight / 5f;

		int points = 32;
		Vector2[] ellipsePoints = new Vector2[points];
		for (int i = 0; i < points; i++) {
			float angle = Mathf.Tau * i / points;
			float x = Mathf.Cos(angle) * radiusX;
			float y = Mathf.Sin(angle) * radiusY;
			ellipsePoints[i] = localPos + new Vector2(x, y);
		}

		DrawPolyline(ellipsePoints, new Color(0, 1, 0, 0.5f), 2f, true);
	}

	public void AttachTo(Sprite2D sprite) {
		_sprite = sprite;
	}
}
