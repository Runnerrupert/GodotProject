using Godot;
using System;

public partial class TestMapGenerator : Node2D {
	private Spawner spawner;
	
	public override void _Ready() {
		spawner = GetNode<Spawner>("Spawner");
		
		PackedScene playerScene = GD.Load<PackedScene>("res://Scenes/Characters/PlayerCharacter.tscn");
		PackedScene enemyScene = GD.Load<PackedScene>("res://Scenes/Characters/EnemyCharacter.tscn");
		PackedScene mainBaseScene = GD.Load<PackedScene>("res://Scenes/Characters/MainBase.tscn");
		
		
		//spawner.Spawn(mainBaseScene, new Vector2(100, 100), "player_buildings");
		
		spawner.Spawn(playerScene, new Vector2(100, 150), "player_units");
	}
}
