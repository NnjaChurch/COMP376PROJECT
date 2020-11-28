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
	[SerializeField] Image health_bar;
	[SerializeField] Image stamina_bar;
	[SerializeField] Image experience_bar;
	[SerializeField] Text player_level_text;

	//------------------------------------------------------------------------Equipment Info-----------------------------------------//
	[SerializeField] UIManager manager_UI; // To get the equipped weapon and armour from the equipment manager

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

		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {

	}

	public void ButtonStatsClick()
	{
		Debug.Log("CharacterUI.ButtonStatsClick()");

		if (!this.gameObject.activeSelf)
		{
			this.gameObject.SetActive(true);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

	public void updatePlayerHealth(int current_health, int max_health) {
		Debug.Log("CharacterUI.updatePlayerHealth()");
		health_bar.fillAmount = (float)current_health / max_health;
		// TODO: Display Numbers?
	}

	public void updatePlayerStamina(int current_stamina, int max_stamina) {
		Debug.Log("CharacterUI.updatePlayerStamina()");
		stamina_bar.fillAmount = (float)current_stamina / max_stamina;
		// TODO: Display Numbers?
	}

	public void updatePlayerExperience(int level, int current_experience, int next_level) {
		Debug.Log("CharacterUI.updatePlayerExperience()");
		experience_bar.fillAmount = (float)current_experience / next_level;
		//experience_text.text = current_experience + "/" + next_level;		TODO: Missing text in Character UI?
		player_level_text.text = "Level " + level;
	}

	public void updateEquippedWeapon() {
		Debug.Log("CharacterUI.updateEquippedWeapon()");

		Weapon equippedWeapon = manager_UI.GetEquippedWeapon();
		equippedWeaponImage.sprite = equipments[equippedWeapon.GetWeaponName()];
		equippedWeaponName.text = equippedWeapon.GetWeaponName();
		equippedWeaponStats.text = "Damage: " + equippedWeapon.GetDamage();
	}

	public void updateEquippedArmour() {
		Debug.Log("CharacterUI.updateEquippedArmour()");

		Armour equippedArmour = manager_UI.GetEquippedArmour();
		equippedArmourImage.sprite = equipments[equippedArmour.GetArmourName()];
		equippedArmourName.text = equippedArmour.GetArmourName();
		equippedArmourStats.text = "Defense: " + equippedArmour.GetDefense();
	}

	public void updatePlayerSkills(int strength, int dexterity, int intelligence) {
		Debug.Log("CharacterUI.updatePlayerSkills()");

		strengthText.text = "Strength: " + strength;
		dexterityText.text = "Dexterity: " + dexterity;
		intelligenceText.text = "Intelligence: " + intelligence;
	}
}
