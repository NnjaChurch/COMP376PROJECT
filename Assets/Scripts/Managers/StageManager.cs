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
	[SerializeField] bool safe_zone;

	public bool Initialize() {
		Debug.Log("Initializing StageManager...");
		return true;
	}

	public void TravelSafeZone() {
		manager_save.SaveGame();
		SceneManager.LoadScene("Safe Zone");
	}

	public void TravelZone(string zone_name) {
		manager_save.SaveGame();
		SceneManager.LoadScene(zone_name);
	}

	public void TravelBossFight() {
		manager_save.SaveGame();
		SceneManager.LoadScene("Boss");
	}

	public bool GetSafeZone() {
		return safe_zone;
	}
}
