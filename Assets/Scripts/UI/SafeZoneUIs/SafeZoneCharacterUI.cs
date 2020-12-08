using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeZoneCharacterUI : MonoBehaviour {
	[SerializeField] UIManager manager_UI; // To get the equipped weapon and armour from the equipment manager

	//------------------------------------------------------------------------Player Info--------------------------------------------//
	[SerializeField] Image health_bar;
	[SerializeField] Text health_text;
	[SerializeField] Image stamina_bar;
	[SerializeField] Text stamina_text;
	[SerializeField] Image experience_bar;
	[SerializeField] Text exp_text;
	[SerializeField] Text player_level_text;
	[SerializeField] Text banked_exp_text;

	//------------------------------------------------------------------------Equipment Info-----------------------------------------//
	[SerializeField] Image equippedWeaponImage;
	[SerializeField] Text equippedWeaponName;
	[SerializeField] Text equippedWeaponStats;

	[SerializeField] Image equippedArmourImage;
	[SerializeField] Text equippedArmourName;
	[SerializeField] Text equippedArmourStats;

	[SerializeField] string[] weaponNames; // The weapon names will correspond to the weapon image of the same index
	[SerializeField] Sprite[] weaponImages;

	[SerializeField] string[] armourNames;
	[SerializeField] Sprite[] armourImages;

	IDictionary<string, Sprite> equipments = new Dictionary<string, Sprite>(); // To keep reference of each weapon/armour and their respective sprites

	//------------------------------------------------------------------------Player Stats Info--------------------------------------//
	[SerializeField] Text skillsText;

	public void Initialize() {
		Debug.Log("Initializing SafeZoneCharacterUI...");
		for (int i = 0; i < weaponNames.Length; i++) {
			equipments.Add(weaponNames[i], weaponImages[i]);
		}
		for (int i = 0; i < armourNames.Length; i++) {
			equipments.Add(armourNames[i], armourImages[i]);
		}
	}

	public void updatePlayerHealth(int current_health, int max_health) {
		//Debug.Log("CharacterUI.updatePlayerHealth()");
		health_bar.fillAmount = (float)current_health / max_health;
		health_text.text = current_health + "/" + max_health;
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		//Debug.Log("CharacterUI.updatePlayerStamina()");
		stamina_bar.fillAmount = (float)current_stamina / max_stamina;
		stamina_text.text = current_stamina + "/" + max_stamina;
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level, int banked_exp) {
		//Debug.Log("CharacterUI.updatePlayerExperience()");
		experience_bar.fillAmount = (float)current_experience / next_level;
		exp_text.text = current_experience + "/" + next_level;
		player_level_text.text = "Level " + level;
		banked_exp_text.text = "Banked Exp: " + banked_exp;
	}

	public void updateEquippedWeapon() {
		//Debug.Log("CharacterUI.updateEquippedWeapon()");

		Weapon equippedWeapon = manager_UI.GetEquippedWeapon();
		equippedWeaponImage.sprite = equipments[equippedWeapon.GetWeaponName()];
		equippedWeaponName.text = equippedWeapon.GetWeaponName();
		equippedWeaponStats.text = "Damage: " + equippedWeapon.GetDamage();
	}

	public void updateEquippedArmour() {
		//Debug.Log("CharacterUI.updateEquippedArmour()");

		Armour equippedArmour = manager_UI.GetEquippedArmour();
		equippedArmourImage.sprite = equipments[equippedArmour.GetArmourName()];
		equippedArmourName.text = equippedArmour.GetArmourName();
		equippedArmourStats.text = "Defense: " + equippedArmour.GetDefense();
	}

	public void updatePlayerStats(string[] stat_names, float[] stat_values) {
		//Debug.Log("CharacterUI.updatePlayerSkills()");

		skillsText.text = "";

		int number_of_skills = stat_names.Length;
		for (int i = 0; i < number_of_skills; i++) {
			skillsText.text += stat_names[i] + ": " + (int)stat_values[i] + "\n";
		}
	}
}
