using Godot;
using System;
using System.Collections.Generic;


public partial class SettingsScreen : Control
{
	private Button audioButton;
	private Button displayButton;
	private Button gameplayButton;
	private Button controlsButton;
	private Button uiButton;
	private Button graphicsButton;
	private Button creditsButton;
	
	private Button applySettingsButton;
	private Button cancelSettingsButton;
	
	private Control audioPanel;
	private Control displayPanel;
	private Control gameplayPanel;
	private Control controlsPanel;
	private Control uiPanel;
	private Control graphicsPanel;
	private Control creditsPanel;
	
	private Dictionary<string, SettingsPanel> settingsPanels = new();
	
	private SettingsManager settingsManager;
	
	public override void _Ready() {
		
		// Managers
		settingsManager = GetNode<SettingsManager>("/root/SettingsManager");
		
		// Buttons
		audioButton = GetNode<Button>("HBoxContainer/VBoxContainer/AudioButton");
		displayButton = GetNode<Button>("HBoxContainer/VBoxContainer/DisplayButton");
		gameplayButton = GetNode<Button>("HBoxContainer/VBoxContainer/GameplayButton");
		controlsButton = GetNode<Button>("HBoxContainer/VBoxContainer/ControlsButton");
		uiButton = GetNode<Button>("HBoxContainer/VBoxContainer/UIButton");
		graphicsButton = GetNode<Button>("HBoxContainer/VBoxContainer/GraphicsButton");
		creditsButton = GetNode<Button>("HBoxContainer/VBoxContainer/CreditsButton");
		
		applySettingsButton = GetNode<Button>("ApplySettingsButton");
		cancelSettingsButton = GetNode<Button>("CancelSettingsButton");
		
		// Panels
		audioPanel = GetNode<Control>("HBoxContainer/PanelContainer/AudioPanel");
		settingsPanels["Audio"] = audioPanel as SettingsPanel;
		
		displayPanel = GetNode<Control>("HBoxContainer/PanelContainer/DisplayPanel");
		settingsPanels["Display"] = displayPanel as SettingsPanel;
		
		gameplayPanel = GetNode<Control>("HBoxContainer/PanelContainer/GameplayPanel");
		settingsPanels["Gameplay"] = gameplayPanel as SettingsPanel;
		
		controlsPanel = GetNode<Control>("HBoxContainer/PanelContainer/ControlsPanel");
		settingsPanels["Controls"] = controlsPanel as SettingsPanel;
		
		uiPanel = GetNode<Control>("HBoxContainer/PanelContainer/UIPanel");
		settingsPanels["UI"] = uiPanel as SettingsPanel;
		
		graphicsPanel = GetNode<Control>("HBoxContainer/PanelContainer/GraphicsPanel");
		settingsPanels["Graphics"] = graphicsPanel as SettingsPanel;
		
		creditsPanel = GetNode<Control>("HBoxContainer/PanelContainer/CreditsPanel");
		settingsPanels["Credits"] = creditsPanel as SettingsPanel;
		
		// Connect Signals
		audioButton.Pressed += () => ShowPanel("Audio");
		displayButton.Pressed += () => ShowPanel("Display");
		gameplayButton.Pressed += () => ShowPanel("Gameplay");
		controlsButton.Pressed += () => ShowPanel("Controls");
		uiButton.Pressed += () => ShowPanel("UI");
		graphicsButton.Pressed += () => ShowPanel("Graphics");
		creditsButton.Pressed += () => ShowPanel("Credits");
		
		applySettingsButton.Pressed += () => ApplyAllSettings();
		cancelSettingsButton.Pressed += () => DiscardAllSettings();
	}
	
	private void ShowPanel(string panelName) {
		foreach (var panel in settingsPanels.Values) {
			panel.Visible = false;
		}
		
		if (settingsPanels.TryGetValue(panelName, out SettingsPanel panelToShow)) {
			panelToShow.Visible = true;
			panelToShow.Initialize();
			panelToShow.RefreshUI();
		}
		
	}
	
	public void ApplyAllSettings() {
		settingsManager.ApplyTempSettings();
		ApplyDisplaySettings();
	}
	
	public void DiscardAllSettings() {
		settingsManager.DiscardTempSettings();
		
		foreach (var panel in settingsPanels.Values) {
			panel.RefreshUI();
		}
	}
	
	public void ApplyDisplaySettings() {
		GD.Print("Applying Display Settings.");
		int mode = (int)settingsManager.GetSetting(settingsManager.settings, "Display", "windowDisplayMode", 1);
		if (mode == 0) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
		} else if (mode == 1) {
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
	}
	
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
