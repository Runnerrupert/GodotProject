using Godot;
using System;

public partial class ControlsScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print("Controls Panel Logic Initialized");
		
		initialized = true;
	}
}
