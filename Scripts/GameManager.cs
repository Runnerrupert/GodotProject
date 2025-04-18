using Godot;
using System;

public partial class GameManager : Node {
	
	public static GameManager instance { get; private set; }

	public string gameVersion = "Version 0.0.1.3 Build Date: 4/17/2025";

	public override void _Ready() {
		instance = this;
	}
	
	public void InitializeStartup() {
		
	} 

	public void InitializeTutorial() {

	}

	public void QuitGame() {
		GetTree().Quit();
	}
}
