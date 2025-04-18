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
	
	private Control audioPanel;
	private Control displayPanel;
	private Control gameplayPanel;
	private Control controlsPanel;
	private Control uiPanel;
	private Control graphicsPanel;
	private Control creditsPanel;
	
	private Dictionary<string, SettingsPanel> settingsPanels = new();
	
	public override void _Ready() {
		
		// Buttons
		audioButton = GetNode<Button>("HBoxContainer/VBoxContainer/AudioButton");
		displayButton = GetNode<Button>("HBoxContainer/VBoxContainer/DisplayButton");
		gameplayButton = GetNode<Button>("HBoxContainer/VBoxContainer/GameplayButton");
		controlsButton = GetNode<Button>("HBoxContainer/VBoxContainer/ControlsButton");
		uiButton = GetNode<Button>("HBoxContainer/VBoxContainer/UIButton");
		graphicsButton = GetNode<Button>("HBoxContainer/VBoxContainer/GraphicsButton");
		creditsButton = GetNode<Button>("HBoxContainer/VBoxContainer/CreditsButton");
		
		// Panels
		audioPanel = GetNode<Control>("HBoxContainer/PanelContainer/AudioPanel");
		if (audioPanel is SettingsPanel audioSettingsPanel)
			{
				settingsPanels["Audio"] = audioSettingsPanel;
			}
			else
			{
				GD.PrintErr("Failed to cast AudioPanel to SettingsPanel");
			}

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
	}
	
	private void ShowPanel(string panelName) {
		foreach (var panel in settingsPanels.Values) {
			panel.Visible = false;
		}
		
		if (settingsPanels.TryGetValue(panelName, out SettingsPanel panelToShow)) {
			panelToShow.Visible = true;
			panelToShow.Initialize();
		}
		
	}
	
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
