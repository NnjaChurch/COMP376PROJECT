using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the main menu behaviour
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class MainMenu : MonoBehaviour {

	[SerializeField] SaveManager manager_save;

	[SerializeField] Button button_new_game;
	[SerializeField] Button button_load_game;

	private void Start() {
		if (manager_save.CheckSave()) {
			button_new_game.interactable = false;
		}
		else {
			button_load_game.interactable = false;
		}
	}

	public void ButtonOptionClick(string option) {
		if (option == "New Game") {
			SceneManager.LoadScene("Zone 1");
		}
		else if (option == "Load Game") {
			SceneManager.LoadScene("Safe Zone");
		}
		else if (option == "Exit") {
			Application.Quit();
		}
	}
}
