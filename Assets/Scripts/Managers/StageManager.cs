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
	[SerializeField] bool SafeZone;

	// Start is called before the first frame update
	void Start() {

		if(SafeZone) {
			// TODO: Loading Safe Zone Info
		}
		else {
			// TODO: Setup Lootable and Enemy Spawns
		}

	}

	// Update is called once per frame
	void Update() {
	}

	public void TravelSafeZone() {
		manager_save.SaveGame();
		SceneManager.LoadScene("Safe Zone");
	}

	public void TravelZone(string zone_name) {
		manager_save.SaveGame();
		SceneManager.LoadScene(zone_name);
	}
}
