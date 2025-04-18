using Godot;
using System;

public partial class SettingsPanel : Control
{
	protected bool initialized = false;
	
	public virtual void Initialize() {
		if (initialized) {
			return;
		}
		
		GD.Print($"{Name} Initialized.");
		initialized = true;
	}
}
