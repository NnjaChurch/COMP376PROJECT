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

	Player player;
	Image health_bar;
	Image stamina_bar;
	Image experience_bar;
	Text experience_text;
	Text player_level_text;
	Text current_zone_text;

	GameObject inventory_ui;

	// Start is called before the first frame update
	void Start() {
		player = FindObjectOfType<Player>();
		health_bar = GameObject.Find("HBar").GetComponent<Image>();
		stamina_bar = GameObject.Find("SBar").GetComponent<Image>();
		experience_bar = GameObject.Find("EXPBar").GetComponent<Image>();
		experience_text = GameObject.Find("XPCurrentText").GetComponent<Text>();
		player_level_text = GameObject.Find("LVLText").GetComponent<Text>();
		current_zone_text = GameObject.Find("ZoneText").GetComponent<Text>();
		//int zone_number = ...;
		//string zone_name = ...;
		//current_zone_text.text = "Zone " + zone_number + ": " + zone_name;
		inventory_ui = GameObject.Find("InventoryPanel");
		inventory_ui.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		health_bar.fillAmount = player.GetPlayerCurrentHealth() / player.GetPlayerBaseHealth();
		stamina_bar.fillAmount = player.GetPlayerCurrentStamina() / player.GetPlayerBaseStamina();

		float current_exp = player.GetPlayerCurrentExperience();
		float base_exp = player.GetPlayerBaseExperience();
		experience_bar.fillAmount = current_exp / base_exp;
		experience_text.text = current_exp + "/" + base_exp;
		player_level_text.text = "Level " + player.GetPlayerLevel();
	}
}
