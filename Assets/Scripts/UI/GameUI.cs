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

	[SerializeField] PlayerStats player_stats;
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

		if (Input.GetKeyDown(KeyCode.B))
        {
			if (!inventory_ui.activeSelf)
            {
				inventory_ui.SetActive(true);
			}
			else
            {
				inventory_ui.SetActive(false);
			}
        }

		if (Input.GetKeyDown(KeyCode.V))
		{
			if (!player_stats_ui.activeSelf)
			{
				player_stats_ui.SetActive(true);
			}
			else
			{
				player_stats_ui.SetActive(false);
			}
		}
	}

	public void ButtonInventoryClick()
    {
		if (!player_stats_ui.activeSelf)
		{
			player_stats_ui.SetActive(true);
		}
		else
		{
			player_stats_ui.SetActive(false);
		}
	}

	public void ButtonStatsClick()
	{
		if (!inventory_ui.activeSelf)
		{
			inventory_ui.SetActive(true);
		}
		else
		{
			inventory_ui.SetActive(false);
		}
	}

	public void updatePlayerHealth()
    {
		health_bar.fillAmount = player_stats.GetCurrentHealth() / player_stats.GetMaxHealth();
		player_stats_ui_script.updatePlayerHealth();
	}

	public void updatePlayerStamina()
	{
		stamina_bar.fillAmount = player_stats.GetCurrentStamina() / player_stats.GetMaxStamina();
		player_stats_ui_script.updatePlayerStamina();
	}

	public void updatePlayerExperience()
	{
		// TODO is GetCurrentNextLevel() the correct function here?
		int current_exp = player_stats.GetCurrentExperience();
		int base_exp = player_stats.GetCurrentNextLevel();
		experience_bar.fillAmount = current_exp / base_exp;
		experience_text.text = current_exp + "/" + base_exp;
		player_level_text.text = "Level " + player_stats.GetCurrentLevel();

		player_stats_ui_script.updatePlayerExperience();
	}
}
