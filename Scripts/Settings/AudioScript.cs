using Godot;
using System;

public partial class AudioScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print("Audio Panel Logic Initialized");
		
		initialized = true;
	}
}
