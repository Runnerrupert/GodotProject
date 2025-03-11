using Godot;
using System;

public partial class TitleScreen : Control
{
	public void GoToSettings() {
		SceneManager.instance.ChangeScene(eSceneNames.SettingsScreen);
	}
}
