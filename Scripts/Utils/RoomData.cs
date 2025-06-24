using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoomData : Node2D {
	
	public List<Marker2D> GetDoors() {
			return GetChildren()
				.OfType<Marker2D>()
				.Where(child => child.Name.ToString().IndexOf("door", StringComparison.OrdinalIgnoreCase) >= 0)
				.ToList();
		}



}
