using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the pause menu User Interface
//	Contributors: 
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class PauseMenu : MonoBehaviour {

	bool game_paused;

	public void Initialize() {
		Debug.Log("Initializing PauseMenu...");
		game_paused = false;
	}

	// Update is called once per frame
	void Update() {

	}

	public void PauseGame() {
		Time.timeScale = 0;
		// TODO: Make Pause Menu Visible
		// Disable Interactivity with other Objects
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		// TODO: Hide Pause Menu
		// Enable Interactivity with other Objects
	}

	public bool GetGamePaused() {
		return game_paused;
	}
}
