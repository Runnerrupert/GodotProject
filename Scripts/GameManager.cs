using Godot;
using System;

public partial class GameManager : Node {

	public string gameVersion = "Version 0.0.1.2 Build Date: 3/11/2025";

	public void QuitGame() {
		GetTree().Quit();
	}
}
