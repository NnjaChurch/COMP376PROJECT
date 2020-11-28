using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the in-game User Interface
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class GameUI : MonoBehaviour {

	[SerializeField] Image health_bar;
	[SerializeField] Image stamina_bar;
	[SerializeField] Image experience_bar;
	[SerializeField] Text experience_text;
	[SerializeField] Text player_level_text;

	// Start is called before the first frame update
	void Start() {
		//int zone_number = ...;
		//string zone_name = ...;
		//current_zone_text.text = "Zone " + zone_number + ": " + zone_name;
	}

	// Update is called once per frame
	void Update() {

	}

	public void updatePlayerHealth(int current_health, int max_health) {
		Debug.Log("GameUI.updatePlayerHealth()");

		health_bar.fillAmount = (float)current_health / max_health;
		// TODO: Add numbers text after bar? In bar?
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		Debug.Log("GameUI.updatePlayerStamina()");

		stamina_bar.fillAmount = (float)current_stamina / max_stamina;
		// TODO: Add numbers text after bar? In bar?
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level) {
		Debug.Log("GameUI.updatePlayerExperience()");

		experience_bar.fillAmount = (float)current_experience / next_level;
		experience_text.text = current_experience + "/" + next_level;
		player_level_text.text = "Level " + level;
	}
}
