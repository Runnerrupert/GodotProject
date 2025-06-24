using Godot;
using System;

public partial class MainBase : Node2D
{	
	[Export] public string MainBaseName = "Main Base";
	
	[Export] public int MaxHealth = 150;
	public int CurrentHealth;
	public Vector2 Dimensions = new Vector2(128, 128);
	
	public bool IsAlive() => CurrentHealth > 0;
	
	// Collision
	protected Area2D hitbox;
	
	// Location where Objects will be scanned
	public Area2D DropOffLocation;
	
	// UI
	public VisualComponent visuals; 
	
	public override void _Ready() {
		CurrentHealth = MaxHealth;
		
		visuals = GetNode<VisualComponent>("VisualComponent");
		visuals.Owner = this;
		
		visuals.SetSprite("res://Assets/Textures/lilGuy.png");
		visuals.SetupEntityUI(MaxHealth, MainBaseName);
		visuals.SetHealthBarValue(CurrentHealth);
		
		CreateHitbox();
		CreateDropOffLocation();
		
		// Console Logs
		GD.Print($"{MainBaseName} spawned with {MaxHealth} HP");
	}
	
	private void Scan() {
		GD.Print($"Scanning...");
	}
	
	public virtual void CreateHitbox() {
		hitbox = new Area2D {
			Name = "Hitbox",
			InputPickable = true,
			CollisionLayer = 1 << 0,
			CollisionMask = 1 << 0
		};
		
		var shape = new RectangleShape2D {
			Size = Dimensions
		};
		
		var collisionShape = new CollisionShape2D {
			Shape = shape
		};
		
		collisionShape.Name = "CollisionShapeHitbox";
		
		hitbox.AddChild(collisionShape);
		AddChild(hitbox);
		
		hitbox.Owner = this;
		
		// Finished Print Statement
		GD.Print($"HitBox Created for {MainBaseName}");
	}
	
	// Creates the Area2D location for object drop off
	public virtual void CreateDropOffLocation() {
		GD.Print("Creating drop off location...");
	}
	
	
	public virtual void TakeDamage(int amount) {
		CurrentHealth -= amount;
		
		if (CurrentHealth <= 0) {
			Die();
		}
		
		visuals.SetHealthBarValue(CurrentHealth);
	}
	
	public virtual void Die() {
		GD.Print($"{MainBaseName} was destroyed.");
		QueueFree();
	}
}
