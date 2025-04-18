using Godot;
using System;

public partial class DisplayScript : SettingsPanel
{
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		OptionButton displayModeButton = GetNode<OptionButton>("OptionButton_DisplayMode");
		displayModeButton.ItemSelected += OnDisplayModeChanged;
		
		GD.Print("Display Panel Logic Initialized");
		
		initialized = true;
	}
	
	private void OnDisplayModeChanged(long index){
		if (index == 0) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
			GD.Print("Switched to Windowed Mode");
		} else if (index == 1) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
			GD.Print("Switched to Fullscreen Mode");
		}
	}
}
