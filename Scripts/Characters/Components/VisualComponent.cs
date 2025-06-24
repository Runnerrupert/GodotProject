using Godot;
using System;

public partial class VisualComponent : Node2D
{
	public Sprite2D Sprite { get; private set; }
	
	public CenterContainer HContainer;
	public VBoxContainer VContainer;
	public Label NameLabel;
	private TextureProgressBar HealthBar;
	
	public override void _Ready() {
		NameLabel = GetNode<Label>("UIAnchor/CenterContainer/VBoxContainer/NameLabel");
		HealthBar = GetNode<TextureProgressBar>("UIAnchor/CenterContainer/VBoxContainer/HealthBar");
		HContainer = GetNode<CenterContainer>("UIAnchor/CenterContainer");
		VContainer = GetNode<VBoxContainer>("UIAnchor/CenterContainer/VBoxContainer");
		
		Name = "VisualComponent";
	}
	
	public void SetSprite(string texturePath) {
		Sprite = new Sprite2D { Name = "MainSprite" };
		Sprite.Texture = GD.Load<Texture2D>(texturePath);
		AddChild(Sprite);
	}
	
	public void CreateHealthBar(int maxHealth) {
		var healthbgTexture = TextureUtils.CreateSolidColorTexture(new Color(0.2f, 0.2f, 0.2f), 100, 10);
		var healthfgTexture = TextureUtils.CreateSolidColorTexture(new Color(0, 1, 0), 100, 10);
		
		HealthBar.TextureUnder = healthbgTexture;
		HealthBar.TextureProgress = healthfgTexture;
		
		HealthBar.MaxValue =  maxHealth;
	}
	
	public void CreateNameLabel(string labelText) {
		NameLabel.Text = labelText;
	}
	
	//public void CenterUIUsingHitbox() {
		//
	//}
	
	public void SetupEntityUI(int maxHealth, string labelText) {
		CreateHealthBar(maxHealth);
		CreateNameLabel(labelText);
	}
	
	public void SetHealthBarValue(int currentHealth) {
		if (HealthBar != null) {
			HealthBar.Value = currentHealth;
		}
	}
}
