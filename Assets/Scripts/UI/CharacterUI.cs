using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the character statistics User Interface
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class CharacterUI : MonoBehaviour {
	// Start is called before the first frame update

	//------------------------------------------------------------------------Player Info--------------------------------------------//
	[SerializeField] PlayerStats player_stats;
	[SerializeField] Image health_bar;
	[SerializeField] Image stamina_bar;
	[SerializeField] Image experience_bar;
	[SerializeField] Text player_level_text;

	//------------------------------------------------------------------------Equipment Info-----------------------------------------//
	[SerializeField] EquipmentManager equipment_manager; // To get the equipped weapon and armour

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

	IDictionary<string, Sprite> equipments = new Dictionary<string, Sprite>();

	//------------------------------------------------------------------------Player Stats Info--------------------------------------//
	[SerializeField] Text strengthText;
	[SerializeField] Text dexterityText;
	[SerializeField] Text intelligenceText;

	void Start() {
		for (int i = 0; i < weaponNames.Length; i++) {
			equipments.Add(weaponNames[i], weaponImages[i]);
		}
		for (int i = 0; i < armourNames.Length; i++) {
			equipments.Add(armourNames[i], armourImages[i]);
		}
	}

	// Update is called once per frame
	void Update() {

	}

	public void updatePlayerHealth(int current_health, int max_health) {
		health_bar.fillAmount = current_health / max_health;
		// TODO: Display Numbers?
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		stamina_bar.fillAmount = current_stamina / max_stamina;
		// TODO: Display Numbers?
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level) {
		experience_bar.fillAmount = current_experience / next_level;
		//experience_text.text = current_experience + "/" + next_level;		TODO: Missing text in Character UI?
		player_level_text.text = "Level " + level;
	}

	public void updateEquippedWeapon() {
		Weapon equippedWeapon = equipment_manager.GetEquippedWeapon();
		equippedWeaponImage.sprite = equipments[equippedWeapon.GetWeaponName()];
		equippedWeaponName.text = equippedWeapon.GetWeaponName();
		equippedWeaponStats.text = "Damage: " + equippedWeapon.GetDamage();
	}

	public void updateEquippedArmour() {
		Armour equippedArmour = equipment_manager.GetEquippedArmour();
		equippedArmourImage.sprite = equipments[equippedArmour.GetArmourName()];
		equippedArmourName.text = equippedArmour.GetArmourName();
		equippedArmourStats.text = "Defense: " + equippedArmour.GetDefense();
	}

	public void updatePlayerSkills() {
		strengthText.text = "Strength: " + player_stats.GetStrength();
		dexterityText.text = "Dexterity: " + player_stats.GetDexterity();
		intelligenceText.text = "Intelligence: " + player_stats.GetIntelligence();
	}
}
