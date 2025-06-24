using Godot;
using System;

public partial class NewGameScreen : Control
{	
	public override void _Ready() {
		GetNode<Button>("Slot1Button").Pressed += () => GameManager.instance.StartNewGame(1);
		GetNode<Button>("Slot2Button").Pressed += () => GameManager.instance.StartNewGame(2);
		GetNode<Button>("Slot3Button").Pressed += () => GameManager.instance.StartNewGame(3);
	}
	
	public void BackButton() {
		SceneManager.instance.ChangeScene(eSceneNames.TitleScreen);
	}
}
