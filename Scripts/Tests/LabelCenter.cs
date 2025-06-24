using Godot;
using System;

public partial class LabelCenter : Node2D
{
	private Label NameLabel;
	private Panel NamePanel;
	private Sprite2D Sprite;
	
	public override void _Ready() {
		NameLabel = GetNode<Label>("TestPlayer/Panel/Label");
		NamePanel = GetNode<Panel>("TestPlayer/Panel");
		Sprite = GetNode<Sprite2D>("TestPlayer/Sprite2D");
		
		NameLabel.Text = "Cameron";
		
		SetAnchorsAndOffset(NameLabel, NamePanel, Sprite);
	}
	
	public static void SetAnchorsAndOffset(Control label, Control parentPanel, Sprite2D sprite) {
		float verticalSeparation = 20f;
		
		Vector2 textureSize = sprite.Texture.GetSize();
		Vector2 finalSize = textureSize * sprite.Scale;
		
		float spriteWidth = finalSize.X;
		float spriteHeight = finalSize.Y;
		
		// Set anchors to top-center
		label.AnchorLeft = 0.5f;
		label.AnchorRight = 0.5f;
		label.AnchorTop = 0.0f;
		label.AnchorBottom = 0.0f;

		// Get panel width (you can also use parentPanel.Size.X)
		
		float halfWidth = label.Size.X / 2f;
		
		// Offset to center horizontally and move up vertically
		label.OffsetLeft = -halfWidth - (spriteWidth / 2f);
		label.OffsetRight = halfWidth - (spriteWidth / 2f);
		
		float verticalOffset = -(spriteHeight / 2f) - parentPanel.Size.Y / 2 - verticalSeparation;
		
		label.OffsetTop = verticalOffset;
		label.OffsetBottom = verticalOffset;
	}

}
