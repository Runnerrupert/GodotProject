using Godot;
using System;

public partial class DisplayScript : SettingsPanel
{
	private SettingsManager settingsManager;
	private OptionButton displayModeButton;
	
	public override void Initialize() {
		if (initialized) {
			return;
		}
		
		settingsManager = GetNode<SettingsManager>("/root/SettingsManager");
		displayModeButton = GetNode<OptionButton>("OptionButton_DisplayMode");
		displayModeButton.ItemSelected += OnDisplayModeChanged;
		
		GD.Print("Display Panel Logic Initialized");
		
		initialized = true;
	}
	
	public override void RefreshUI() {
		
		if (settingsManager == null) {
			settingsManager = GetNode<SettingsManager>("/root/SettingsManager");
		}
		
		int currentIndex = (int)settingsManager.GetSetting(settingsManager.settings, "Display", "windowDisplayMode", 1);
		displayModeButton.Select(currentIndex);
		
		GD.Print("Display Panel Refreshed");
	}
	
	private void OnDisplayModeChanged(long index){
		settingsManager.SetSetting(settingsManager.tempSettings, "Display", "windowDisplayMode", index);
		GD.Print(index == 0 ? "Set to Windowed Mode" : "Set to Fullscreen Mode");
	}
}
