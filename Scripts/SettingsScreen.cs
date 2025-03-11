using Godot;
using System;

public partial class SettingsScreen : Control
{
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
