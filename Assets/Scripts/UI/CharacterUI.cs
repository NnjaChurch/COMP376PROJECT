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
		for (int i = 0; i < weaponNames.Length; i++)
        {
			equipments.Add(weaponNames[i], weaponImages[i]);
        }
		for (int i = 0; i < armourNames.Length; i++)
		{
			equipments.Add(armourNames[i], armourImages[i]);
		}
	}

	// Update is called once per frame
	void Update() {

	}

	public void updatePlayerHealth()
	{
		health_bar.fillAmount = player_stats.GetCurrentHealth() / player_stats.GetMaxHealth();

	}

	public void updatePlayerStamina()
	{
		stamina_bar.fillAmount = player_stats.GetCurrentStamina() / player_stats.GetMaxStamina();

	}

	public void updatePlayerExperience()
	{
		// TODO is GetCurrentNextLevel() the correct function here?
		int current_exp = player_stats.GetCurrentExperience();
		int base_exp = player_stats.GetCurrentNextLevel();
		experience_bar.fillAmount = current_exp / base_exp;
		player_level_text.text = "Level " + player_stats.GetCurrentLevel();
	}

	public void updateEquippedWeapon()
    {
		Weapon equippedWeapon = equipment_manager.GetEquippedWeapon();
		equippedWeaponImage.sprite = equipments[equippedWeapon.GetWeaponName()];
		equippedWeaponName.text = equippedWeapon.GetWeaponName();
		equippedWeaponStats.text = "Damage: " + equippedWeapon.GetDamage();
	}

	public void updateEquippedArmour()
    {
		Armour equippedArmour = equipment_manager.GetEquippedArmour();
		equippedArmourImage.sprite = equipments[equippedArmour.GetArmourName()];
		equippedArmourName.text = equippedArmour.GetArmourName();
		equippedArmourStats.text = "Defense: " + equippedArmour.GetDefense();
	}

	public void updatePlayerSkills()
    {
		strengthText.text = "Strength: " + player_stats.GetStrength();
		dexterityText.text = "Dexterity: " + player_stats.GetDexterity();
		intelligenceText.text = "Intelligence: " + player_stats.GetIntelligence();
	}
}
