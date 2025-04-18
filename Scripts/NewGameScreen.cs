using Godot;
using System;

public partial class NewGameScreen : Control
{
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
