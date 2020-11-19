using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	// Weapon References
	[SerializeField] Weapon weapon_knife;
	[SerializeField] Weapon weapon_bat;
	[SerializeField] Weapon weapon_shovel;
	[SerializeField] Weapon weapon_rake;

	// Armour References
	[SerializeField] Armour armour_light;
	[SerializeField] Armour armour_medium;
	[SerializeField] Armour armour_heavy;

	// Manager Class References
	[SerializeField] PlayerManager manager_player;

	// Equipped Item References
	Weapon active_weapon;
	Armour active_armour;

	public void TryAttack() {

	}

	public Weapon GetEquippedWeapon() {
		return active_weapon;
	}

	public Armour GetEquippedArmour() {
		return active_armour;
	}

	public void EquipWeapon(string item) {
		if(item.Equals(weapon_bat.GetWeaponName())) {
			active_weapon.gameObject.SetActive(false);
			active_weapon = weapon_bat;
			active_weapon.gameObject.SetActive(true);
			
		}
		else if(item.Equals(weapon_knife.GetWeaponName())) {
			active_weapon.gameObject.SetActive(false);
			active_weapon = weapon_knife;
			active_weapon.gameObject.SetActive(true);
		}
		else if(item.Equals(weapon_shovel.GetWeaponName())) {
			active_weapon.gameObject.SetActive(false);
			active_weapon = weapon_shovel;
			active_weapon.gameObject.SetActive(true);
		}
		else if(item.Equals(weapon_rake.GetWeaponName())) {
			active_weapon.gameObject.SetActive(false);
			active_weapon = weapon_rake;
			active_weapon.gameObject.SetActive(true);
		}
		else {
			Debug.Log("Unable to equip passed weapon");
		}
		
	}
	public void EquipArmour(string item) {
		if (item.Equals(armour_light.GetArmourName())) {
			active_armour.gameObject.SetActive(false);
			active_armour = armour_light;
			active_armour.gameObject.SetActive(true);
		}
		else if(item.Equals(armour_medium.GetArmourName())) {
			active_armour.gameObject.SetActive(false);
			active_armour = armour_medium;
			active_armour.gameObject.SetActive(true);
		}
		else if(item.Equals(armour_heavy.GetArmourName())) {
			active_armour.gameObject.SetActive(false);
			active_armour = armour_heavy;
			active_armour.gameObject.SetActive(true);
		}
		else {
			Debug.Log("Unable to equip passed armour");
		}

	}

	// -------------------------------------------------------------------------------------------------------------------------------------------- //
	// TODO: Update Checks to use the stored names instead of an arbitrary string using weaponName.Equals(weapon_knife.GetWeaponName()) or armourName.Equals(armour_light.GetArmourName())
	public int GetWeaponWeight(string weaponName) {
		if (weaponName == "Knife") {
			return (int)weapon_knife.GetWeight();
		}
		else if (weaponName == "Bat") {
			return (int)weapon_bat.GetWeight();
		}
		else if (weaponName == "Shovel") {
			return (int)weapon_shovel.GetWeight();
		}
		else if (weaponName == "Rake") {
			return (int)weapon_rake.GetWeight();
		}

		return -1;
	}

	public int GetArmourWeight(string armourName) {
		if (armourName == "Light Armour") {
			return (int)armour_light.GetWeight();
		}
		else if (armourName == "Medium Armour") {
			return (int)armour_medium.GetWeight();
		}
		else if (armourName == "Heavy Armour") {
			return (int)armour_heavy.GetWeight();
		}

		return -1;
	}
}
