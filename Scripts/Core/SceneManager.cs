using Godot;
using System;
using System.Collections.Generic;

public enum eSceneNames {
	TitleScreen = 10,
	LoadGameScreen = 20,
	NewGameScreen = 30,
	SettingsScreen = 40,
	TutorialMission = 100
}

public partial class SceneManager : Node {

	public static SceneManager instance;

	public Dictionary<eSceneNames, SceneData> sceneDictionary = new Dictionary<eSceneNames, SceneData>() {
		{eSceneNames.TitleScreen, new SceneData ("Title Screen", "res://Scenes/UI/TitleScreen.tscn")},
		{eSceneNames.LoadGameScreen, new SceneData ("Load Screen", "res://Scenes/UI/LoadGameScreen.tscn")},
		{eSceneNames.NewGameScreen, new SceneData ("New Game Screen", "res://Scenes/UI/NewGameScreen.tscn")},
		{eSceneNames.SettingsScreen, new SceneData ("Settings Screen", "res://Scenes/UI/SettingsScreen.tscn")},
		{eSceneNames.TutorialMission, new SceneData ("Tutorial Mission", "res://Scenes/Missions/TutorialMission_1.tscn")}
	};

	public override void _Ready() {
		instance = this;
	}

	public void ChangeScene(eSceneNames sceneName) {
		string myPath = sceneDictionary[sceneName].path;
		GetTree().ChangeSceneToFile(myPath);
	}
}
