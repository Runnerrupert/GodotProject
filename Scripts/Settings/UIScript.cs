using Godot;
using System;

public partial class UIScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}

		GD.Print("UI Panel Logic Initialized");
		
		initialized = true;
	}
}
