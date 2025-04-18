using Godot;
using System;

public partial class CreditsScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print("Credits Panel Logic Initialized");
		
		initialized = true;
	}
}
