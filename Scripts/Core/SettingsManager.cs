using Godot;
using System;
using System.Collections.Generic;

public partial class SettingsManager : Node
{
	private readonly string _savePath = "user://settings.cfg";
	private ConfigFile _config = new ConfigFile();
	
	public Dictionary<string, Dictionary<string, Variant>> settings = new(); // Possible Change: Instantiate only when it is required
	public Dictionary<string, Dictionary<string, Variant>> tempSettings = new();
	
	public override void _Ready(){
		LoadSettings();
	}
	
	public void LoadSettings(){
		settings.Clear();
		
		var err = _config.Load(_savePath);
		
		if (err != Error.Ok) {
			GD.Print("No settings file found. Using defaults.");
			InitializeDefaults();
			return;
		}
		
		foreach(string section in _config.GetSections()) {
			settings[section] = new Dictionary<string, Variant>();
			foreach(string key in _config.GetSectionKeys(section)) {
				settings[section][key] = _config.GetValue(section, key);
			}
		}
		
		// Applies any settings that may be missing after initial file creation
		ApplyMissingDefaults();
	}
	
	public void SaveSettings(){
		foreach (var section in settings) {
			foreach (var pair in section.Value) {
				_config.SetValue(section.Key, pair.Key, pair.Value);
			}
		}
		_config.Save(_savePath);
	}
	
	// Change: Merged to work for both SetSetting and setTempSetting - Requires the Dictionary to be passed
	public void SetSetting(Dictionary<string, Dictionary<string, Variant>> currentDict, string section, string key, Variant value) {
		if (!currentDict.ContainsKey(section)) {
			currentDict[section] = new Dictionary<string, Variant>();
		}
		currentDict[section][key] = value;
	}
	
	// Change: Merged to work for both GetSetting and getTempSetting - Requires the Dictionary to be passed
	public Variant GetSetting(Dictionary<string, Dictionary<string, Variant>> currentDict, string section, string key, Variant defaultValue) {
		if (currentDict.TryGetValue(section, out var sectionDict) && sectionDict.TryGetValue(key, out var value)) {
			return value;
		}
		return defaultValue;
	}
	
	public void ApplyTempSettings() {
		foreach (var section in tempSettings) {
			foreach(var pair in section.Value) {
				SetSetting(settings, section.Key, pair.Key, pair.Value);
			}
		}
		SaveSettings();
		tempSettings.Clear();
	}
	
	public void DiscardTempSettings() {
		tempSettings.Clear();
		GD.Print("Temporary Settings Cleared.");
	}
	
	// Initialize the default settings for a new user
	private void InitializeDefaults() {
		ApplyDisplayDefaults();
	}
	
	private void ApplyDisplayDefaults() {
		SetSetting(settings, "Display", "windowDisplayMode", 1); // 0 = Windowed, 1 = Fullscreen
	}
	
	// Write ApplyAudioSettings, ApplyGameplaySettings, ApplyControlsSetttings, ApplyUISettings, and ApplyGraphicsSettings
	
	private void ApplyMissingDefaults() {
		SetDefaultIfMissing("Display", "windowDisplayMode", 1);
	}
	
	private void SetDefaultIfMissing(string section, string key, Variant defaultValue) {
		if (!settings.ContainsKey(section) || !settings[section].ContainsKey(key)) {
			SetSetting(settings, section, key, defaultValue);
		}
	}
}
