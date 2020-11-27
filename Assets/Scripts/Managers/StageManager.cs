using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ----------------------------------------------------------------------------------------------------
//	Description: Class that manages loading the stage and all functions that must be done on load and unload
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class StageManager : MonoBehaviour {

	// Manager References
	[SerializeField] SaveManager manager_save;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		// Temp KeyChecks to Test Scene Switching
		if(Input.GetKey(KeyCode.F1)) {
			manager_save.SaveGame();
			SceneManager.LoadScene("Zone 1");
		}
		if(Input.GetKey(KeyCode.F2)) {
			manager_save.SaveGame();
			SceneManager.LoadScene("Zone 2");
		}
		if(Input.GetKey(KeyCode.F3)) {
			manager_save.SaveGame();
			SceneManager.LoadScene("Zone 3");
		}
	}

	
}
