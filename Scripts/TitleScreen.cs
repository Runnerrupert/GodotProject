using Godot;
using System;

public partial class TitleScreen : Control
{
	
	public override void _Ready() {
		SetButtonSize();
		PrintButtonNamesRecursive(GetNode<VBoxContainer>("CenterContainer/VBoxContainer"));
	}
	
	private void PrintButtonNamesRecursive(Node node) {
		foreach (Node child in node.GetChildren()) {
			GD.Print($"Checking: {child.Name} - {child.GetType()}");
			
			if (child is Button button) {
				GD.Print($"Button found: {button.Name}");
			}
			
			if (child.GetChildCount() > 0) {
				PrintButtonNamesRecursive(child);
			}
		}
	}
	
	public void SetButtonSize() {
		var container = GetNode<VBoxContainer>("CenterContainer/VBoxContainer");
		var screenSize = GetViewport().GetVisibleRect().Size;
		
		foreach (var child in container.GetChildren()) {
			
			if (child is Button button) {
				button.CustomMinimumSize = new Vector2(screenSize.X / 6, screenSize.Y / 20);
				
				button.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;
				
				GD.Print($"{button.Name}: {button.CustomMinimumSize}, Flags: {button.SizeFlagsHorizontal}");

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
