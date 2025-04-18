using Godot;
using System;
using System.Collections.Generic;

public enum eSceneNames {
	TitleScreen = 10,
	LoadGameScreen = 20,
	NewGameScreen = 30,
	SettingsScreen = 40
}

public partial class SceneManager : Node {

	public static SceneManager instance;

	public Dictionary<eSceneNames, SceneData> sceneDictionary = new Dictionary<eSceneNames, SceneData>() {
		{eSceneNames.TitleScreen, new SceneData ("Title Screen", "res://Scenes/TitleScreen.tscn")},
		{eSceneNames.LoadGameScreen, new SceneData ("Load Screen", "res://Scenes/LoadGameScreen.tscn")},
		{eSceneNames.NewGameScreen, new SceneData ("New Game Screen", "res://Scenes/NewGameScreen.tscn")},
		{eSceneNames.SettingsScreen, new SceneData ("Settings Screen", "res://Scenes/SettingsScreen.tscn")}
	};

	public override void _Ready() {
		instance = this;
	}

	public void ChangeScene(eSceneNames sceneName) {
		string myPath = sceneDictionary[sceneName].path;
		GetTree().ChangeSceneToFile(myPath);
	}
}
