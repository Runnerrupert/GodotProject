using Godot;
using System;

public partial class TutorialMission_1 : Node2D
{
	private Spawner spawner;
	
	public override void _Ready() {
		spawner = GetNode<Spawner>("Spawner");
		
		PackedScene playerScene = GD.Load<PackedScene>("res://Scenes/Characters/PlayerCharacter.tscn");
		PackedScene enemyScene = GD.Load<PackedScene>("res://Scenes/Characters/EnemyCharacter.tscn");
		PackedScene mainBaseScene = GD.Load<PackedScene>("res://Scenes/Characters/MainBase.tscn");
		
		
		spawner.Spawn(mainBaseScene, new Vector2(100, 100), "player_buildings");
		
		spawner.Spawn(playerScene, new Vector2(100, 200), "player_units");
		spawner.Spawn(playerScene, new Vector2(100, 200), "player_units");
		spawner.Spawn(playerScene, new Vector2(100, 200), "player_units");
		spawner.Spawn(playerScene, new Vector2(100, 200), "player_units");
		spawner.Spawn(playerScene, new Vector2(100, 200), "player_units");
		
		spawner.Spawn(enemyScene, new Vector2(850, 250), "enemy_units");
		spawner.Spawn(enemyScene, new Vector2(800, 250), "enemy_units");
		spawner.Spawn(enemyScene, new Vector2(750, 250), "enemy_units");
		spawner.Spawn(enemyScene, new Vector2(700, 250), "enemy_units");
		spawner.Spawn(enemyScene, new Vector2(650, 250), "enemy_units");
		spawner.Spawn(enemyScene, new Vector2(600, 250), "enemy_units");
	}
}
