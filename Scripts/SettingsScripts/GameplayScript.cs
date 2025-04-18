using Godot;
using System;

public partial class GameplayScript : SettingsPanel
{
	
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print("Gameplay Panel Logic Initialized");
		
		initialized = true;
	}
}
