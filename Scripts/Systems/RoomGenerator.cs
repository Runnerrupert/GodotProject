using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class RoomGenerator : Node2D
{
	[Export] public PackedScene StartRoomScene;
	[Export] public PackedScene BasicRoomScene;

	private List<RoomData> placedRooms = new();

	public override void _Ready()
	{
		GenerateMap();
	}

	private void GenerateMap()
	{
		// Step 1: Spawn Start Room at origin
		var startRoom = StartRoomScene.Instantiate<RoomData>();
		AddChild(startRoom);
		startRoom.Position = Vector2.Zero;
		placedRooms.Add(startRoom);

		// Step 2: Grab its first door
		var startDoor = startRoom.GetDoors().First();

		// Step 3: Spawn the next room
		var nextRoom = BasicRoomScene.Instantiate<RoomData>();
		AddChild(nextRoom);

		var nextDoor = nextRoom.GetDoors().First();

		// Step 4: Align next room’s door to connect to the first room’s door
		var globalStartDoorPos = startRoom.ToGlobal(startDoor.Position);
		var offset = globalStartDoorPos - nextDoor.Position;
		nextRoom.Position = offset;

		placedRooms.Add(nextRoom);
	}
}
