using Godot;
using System;
using System.Collections.Generic;

public partial class Character : Node2D
{
	[Export] public string CharacterName = "Trash Unit";
	[Export] public int Level = 1;
	[Export] public int MaxHealth = 10;
	[Export] public int BaseDamage = 1;
	
	// Runtime Stats
	public int CurrentHealth;
	public float AttackCooldown = 0.5f;
	private float attackTimer = 0f;
	
	// Universal Traits
	[Export] public float MovementSpeed = 100f;
	[Export] public float AttackRange = 200f;
	
	public bool IsAlive => CurrentHealth > 0;
	
	// UI
	private VisualComponent visuals;
	
	// Collision
	protected Area2D hitbox;
	
	// Movement
	private NavigationAgent2D _agent;
	private Vector2 _targetPosition;
	public Vector2 FinalDestination;
	private bool hasReachedDestination = true;
	public bool HasReachedDestination() => hasReachedDestination;
	
	public List<Character> OverlappingCharacters = new();
	
	// Combat
	public CombatComponent Combat { get; private set; }
	public Character CurrentTarget => Combat?.Target;
	
	// Separation
	public SeparationBehavior Separation { get; private set; }
	protected Area2D separationArea;
	
	// AttackRangeDetector
	public AttackRangeDetector AttackRangeDetector { get; private set; }
	
	public override void _Ready() {
		// Create Navigation Node
		_agent = GetNode<NavigationAgent2D>("NavigationAgent2D");
		_agent.AvoidanceEnabled = true;
		
		// Set up Combat Component
		Combat = new CombatComponent { CharacterOwner = this };
		AddChild(Combat);
		
		// Set up Separation Behavior
		Separation = new SeparationBehavior { CharacterOwner = this };
		AddChild(Separation);
		
		// Set Initial Stats
		CurrentHealth = MaxHealth;
		
		// UI Nodes
		visuals = GetNode<VisualComponent>("VisualComponent");
		
		visuals.SetSprite("res://Assets/Textures/lilGuy.png");
		visuals.SetupEntityUI(MaxHealth, CharacterName);
		visuals.SetHealthBarValue(CurrentHealth);
		
		visuals.NameLabel.Text = "The Chinese Guy Working Next Door";
		
		// Console Logs
		GD.Print($"{CharacterName} spawned with {MaxHealth} HP and {BaseDamage} DMG");
		
		// Create Hitbox
		CreateHitbox();
		
		// Create SeparationArea
		CreateSeparationArea();
		
		// Create AttackRangeDetector
		CreateAttackRangeDetector();
	}
	
	public override void _Process(double delta) {
		Combat.Update(delta);
		// Movement
		if (_agent != null && !_agent.IsNavigationFinished()) {
			Vector2 nextPathPoint = _agent.GetNextPathPosition();
			Vector2 direction = (nextPathPoint - GlobalPosition).Normalized();
			GlobalPosition += direction * MovementSpeed * (float)delta;
			hasReachedDestination = false;
		} else if (!hasReachedDestination) {
			hasReachedDestination = true;
			if (Combat.State == CombatState.Moving) {
				Combat.ChangeCombatState(CombatState.Idle);
			}
		}
	}
	
	
	public virtual void CreateSeparationArea() {
		separationArea = new Area2D {
			Name = "SeparationArea",
			CollisionLayer = 1 << 1,
			CollisionMask = 1 << 1
		};
		var shape = new RectangleShape2D {
			Size = new Vector2(32, 48) // Default is 32, 48
		};
		var collisionShape = new CollisionShape2D {
			Shape = shape
		};
		
		collisionShape.Name = "CollisionShapeSeparator";
		
		separationArea.AddChild(collisionShape);
		AddChild(separationArea);
		
		separationArea.Owner = this;
		
		separationArea.AreaEntered += Separation.OnAreaEntered;
		separationArea.AreaExited += Separation.OnAreaExited;
	}
	
	public virtual void CreateHitbox() {
		
		GD.Print($"HitBox Created for {CharacterName}");
		hitbox = new Area2D {
			Name = "Hitbox",
			InputPickable = true,
			CollisionLayer = 1 << 0,
			CollisionMask = 1 << 0
		};
		
		var shape = new RectangleShape2D {
			Size = new Vector2(32, 48) // Default is 32, 48
		};
		
		var collisionShape = new CollisionShape2D {
			Shape = shape
		};
		
		collisionShape.Name = "CollisionShapeHitbox";
		
		hitbox.AddChild(collisionShape);
		AddChild(hitbox);
		
		hitbox.Owner = this;
	}
	
	public virtual void CreateAttackRangeDetector() {
		AttackRangeDetector = new AttackRangeDetector {
			Name = "AttackRangeArea",
			CollisionLayer = 1 << 2,
			CollisionMask = 1 << 0
		};
		
		var collisionShape = new CollisionShape2D {
			Shape = new CircleShape2D {
				Radius = AttackRange
			}
		};
		AttackRangeDetector.AddChild(collisionShape);
		
		AddChild(AttackRangeDetector);
		AttackRangeDetector.CharacterOwner = this;
	}
	
	public virtual void TakeDamage(int amount) {
		CurrentHealth -= amount;
		
		if (CurrentHealth <= 0) {
			Die();
		}
		
		visuals.SetHealthBarValue(CurrentHealth);
	}
	
	public virtual void Die() {
		GD.Print($"{CharacterName} has died.");
		QueueFree();
	}
	
	// Movement Methods
	public void MoveTo(Vector2 destination) {
		_agent.TargetPosition = destination;
		hasReachedDestination = false;
	}
	
	public void SetFinalDestination(Vector2 destination) {
		FinalDestination = destination;
	}
	
	public void StopMoving() {
		if (_agent != null) {
			_agent.SetTargetPosition(GlobalPosition);
		}
	}
	
	public virtual bool IsEnemy(Character other) {
		if (this is PlayerCharacter && other is EnemyCharacter) {
			return true;
		} else if (this is EnemyCharacter && other is PlayerCharacter) {
			return true;
		}
		return false;
	}
}
