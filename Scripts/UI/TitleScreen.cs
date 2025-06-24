using Godot;
using System;

public partial class TitleScreen : Control
{
	
	public override void _Ready() {
		SetButtonSize();
	}
	
	
	public void SetButtonSize() {
		var container = GetNode<VBoxContainer>("CenterContainer/VBoxContainer");
		var screenSize = GetViewport().GetVisibleRect().Size;
		
		foreach (var child in container.GetChildren()) {
			
			if (child is Button button) {
				button.CustomMinimumSize = new Vector2(screenSize.X / 6, screenSize.Y / 20);
				
				button.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
			}
		}
	}
	
	public void GoToSettings() {
		SceneManager.instance.ChangeScene(eSceneNames.SettingsScreen);
	}
	
	public void GoToNewGameScreen() {
		SceneManager.instance.ChangeScene(eSceneNames.NewGameScreen);
	}
	
	public void GoToLoadGameScreen() {
		SceneManager.instance.ChangeScene(eSceneNames.LoadGameScreen);
	}
	
	public void OnQuitButtonPressed() {
		GameManager.instance.QuitGame();
	}
}
