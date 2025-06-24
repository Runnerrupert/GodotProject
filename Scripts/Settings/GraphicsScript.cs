using Godot;
using System;

public partial class GraphicsScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print("Graphics Panel Logic Initialized");
		
		initialized = true;
	}
}
