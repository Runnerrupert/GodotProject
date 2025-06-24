using Godot;
using System;

public partial class LoadGameScreen : Control
{
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
