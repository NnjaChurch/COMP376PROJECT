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
	[SerializeField] Text current_zone_text;

	[SerializeField] GameObject inventory_ui;
	[SerializeField] GameObject player_stats_ui;

	[SerializeField] CharacterUI player_stats_ui_script; // Reference to the script attached to the player

	// Start is called before the first frame update
	void Start() {
		//int zone_number = ...;
		//string zone_name = ...;
		//current_zone_text.text = "Zone " + zone_number + ": " + zone_name;

		inventory_ui.SetActive(false);
		player_stats_ui.SetActive(false);
	}

	// Update is called once per frame
	void Update() {

	}

	public void ButtonInventoryClick() {
		Debug.Log("GameUI.ButtonInventoryClick()");

		if (!inventory_ui.activeSelf)
		{
			inventory_ui.SetActive(true);
		}
		else
		{
			inventory_ui.SetActive(false);
		}
	}

	public void ButtonBClick()
	{
		Debug.Log("GameUI.BClick()");

		if (!inventory_ui.activeSelf)
		{
			inventory_ui.SetActive(true);
		}
		else
		{
			inventory_ui.SetActive(false);
		}
	}

	public void ButtonStatsClick() 
	{
		Debug.Log("GameUI.ButtonStatsClick()");

		if (!player_stats_ui.activeSelf)
		{
			player_stats_ui.SetActive(true);
		}
		else
		{
			player_stats_ui.SetActive(false);
		}
	}

	public void ButtonVClick()
    {
		Debug.Log("GameUI.VClick()");

		if (!player_stats_ui.activeSelf)
		{
			player_stats_ui.SetActive(true);
		}
		else
		{
			player_stats_ui.SetActive(false);
		}
	}

	public void updatePlayerHealth(int current_health, int max_health) {
		Debug.Log("GameUI.updatePlayerHealth()");

		health_bar.fillAmount = (float)current_health / max_health;
		// TODO: Add numbers text after bar? In bar?
		player_stats_ui_script.updatePlayerHealth(current_health, max_health);
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		Debug.Log("GameUI.updatePlayerStamina()");

		stamina_bar.fillAmount = (float)current_stamina / max_stamina;
		// TODO: Add numbers text after bar? In bar?
		player_stats_ui_script.updatePlayerStamina(current_stamina, max_stamina);
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level) {
		Debug.Log("GameUI.updatePlayerExperience()");

		experience_bar.fillAmount = (float)current_experience / next_level;
		experience_text.text = current_experience + "/" + next_level;
		player_level_text.text = "Level " + level;

		player_stats_ui_script.updatePlayerExperience(level, current_experience, next_level);
	}
}
