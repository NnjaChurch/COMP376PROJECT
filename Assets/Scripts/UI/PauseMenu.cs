using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the pause menu User Interface
//	Contributors: 
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class PauseMenu : MonoBehaviour {

	[SerializeField] UIManager manager_ui;
	[SerializeField] GameObject pause_menu;
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
		// Disable Interactivity with other Objects

		pause_menu.SetActive(true);
		game_paused = true;
		manager_ui.DisableButtons();
	}

	public void ResumeGame() {
		Time.timeScale = 1;
		// Enable Interactivity with other Objects

		pause_menu.SetActive(false);
		game_paused = false;
		manager_ui.EnableButtons();
	}

	public void QuitGame()
    {
		// TODO Exit to main menu?

		SceneManager.LoadScene("Main Menu");
    }

	public bool GetGamePaused() {
		return game_paused;
	}
}
