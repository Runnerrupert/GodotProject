using Godot;
using System;

public partial class GameManager : Node {
	
	public static GameManager instance { get; private set; }

	public string gameVersion = "Version 0.0.1.4 Build Date: 5/13/2025";
	
	public int SelectedSlot { get; private set; } = -1;

	public override void _Ready() {
		instance = this;
	}
	
	public void InitializeStartup() {
		
	} 

	public void InitializeTutorial() {
		GD.Print("Loading tutorial mission...");
		SceneManager.instance.ChangeScene(eSceneNames.TutorialMission);
	}
	
	public void StartNewGame(int slot) {
		GD.Print($"Starting new game on slot {slot}");
		SelectedSlot = slot;
		
		InitializeTutorial();
	}

	public void QuitGame() {
		GetTree().Quit();
	}
}
