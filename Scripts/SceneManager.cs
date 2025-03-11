using Godot;
using System;
using System.Collections.Generic;

public enum eSceneNames {
	TitleScreen = 10,
	LoadScreen = 20,
	SettingsScreen = 30
}

public partial class SceneManager : Node {

	public static SceneManager instance;

	public Dictionary<eSceneNames, SceneData> sceneDictionary = new Dictionary<eSceneNames, SceneData>() {
		{eSceneNames.TitleScreen, new SceneData ("Title Screen", "res://Scenes/TitleScreen.tscn")},
		{eSceneNames.LoadScreen, new SceneData ("Load Screen", "res://Scenes/LoadScreen.tscn")},
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
