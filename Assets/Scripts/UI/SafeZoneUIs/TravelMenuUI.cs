using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelMenuUI : MonoBehaviour {
	[SerializeField] UIManager manager_UI;
	[SerializeField] Button zone2_button;
	[SerializeField] Button zone3_button;

	public void Initialize() {
		Debug.Log("Initializing TravelMenuUI...");
		if (!manager_UI.GetZone2Unlocked()) {
			zone2_button.interactable = false;
		}

		if (!manager_UI.GetZone3Unlocked()) {
			zone3_button.interactable = false;
		}
	}

	public void Zone1ButtonClick() {
		manager_UI.TravelZone("Zone 1");
	}

	public void Zone2ButtonClick() {
		manager_UI.TravelZone("Zone 2");
	}

	public void Zone3ButtonClick() {
		manager_UI.TravelZone("Zone 3");
	}
}
