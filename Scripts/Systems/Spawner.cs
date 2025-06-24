using Godot;
using System;

public partial class Spawner : Node
{
	[Export] public Node2D SpawnParent;
	[Export] public bool AutoAddToSceneTree = true;
	[Export] public bool EnableYSort = true;
	
	public override void _Ready() {
		// Added ---
		if (SpawnParent == null) {
			
			Node parentNode = GetParent();
			while (parentNode != null && parentNode is not Node2D) {
				parentNode = parentNode.GetParent();
			}
			if (parentNode is Node2D node2DParent) {
				SpawnParent = node2DParent;
			} else {
				SpawnParent = new Node2D();
				SpawnParent.Name = "AutoSpawnParent";
				AddChild(SpawnParent);
			}
		}
		
		if (EnableYSort) {
			SpawnParent.YSortEnabled = true;
		}
	}
	
	public Node2D Spawn(PackedScene scene, Vector2 position, string group = "") {
		if (scene == null) {
			GD.PushError("Spawner: Cannot spawn null scene.");
			return null;
		}
		
		Node2D instance = scene.Instantiate<Node2D>();
		instance.GlobalPosition = position;
		
		if (!string.IsNullOrEmpty(group)) {
			instance.AddToGroup(group);
		}
		
		if (AutoAddToSceneTree) {
			if (SpawnParent != null) {
				SpawnParent.AddChild(instance);
			} else {
				GD.PushError("Spawner: SpawnParent is not assigned and could not be auto-resolved.");
			}
		}
		return instance;
	}
}
