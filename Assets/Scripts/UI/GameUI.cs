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

	[SerializeField] UIManager manager_UI;

	[SerializeField] Image health_bar;
	[SerializeField] Text health_text;
	[SerializeField] Image stamina_bar;
	[SerializeField] Text stamina_text;
	[SerializeField] Image experience_bar;
	[SerializeField] Text experience_text;
	[SerializeField] Text player_level_text;
	[SerializeField] Text banked_exp_text;

	public void Initialize() {
		Debug.Log("Initializing GameUI...");
		// TODO: Necessary GameUI Initialization
	}

	public void ToggleInventoryUI()
    {
		manager_UI.ToggleInventoryUI();
    }

	public void ToggleStatsUI()
	{
		manager_UI.ToggleStatsUI();
	}

	public void updatePlayerHealth(int current_health, int max_health) {
		//Debug.Log("GameUI.updatePlayerHealth()");

		health_bar.fillAmount = (float)current_health / max_health;
		health_text.text = current_health + "/" + max_health;
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		//Debug.Log("GameUI.updatePlayerStamina()");

		stamina_bar.fillAmount = (float)current_stamina / max_stamina;
		stamina_text.text = current_stamina + "/" + max_stamina;
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level, int banked_exp) {
		//Debug.Log("GameUI.updatePlayerExperience()");

		experience_bar.fillAmount = (float)current_experience / next_level;
		experience_text.text = current_experience + "/" + next_level;
		player_level_text.text = "Level " + level;
		banked_exp_text.text = "Banked Exp: " + banked_exp;
	}
}
