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
	[SerializeField] SaveManager manager_save;

	// Equipped Item References
	Weapon active_weapon;
	Armour active_armour;

	private void Start() {
		if(manager_save.CheckSave()) {
			List<int> equipment_load = manager_save.LoadEquipment();

			// Weapon Loading
			weapon_knife.SetWeaponTier(equipment_load[0]);
			weapon_bat.SetWeaponTier(equipment_load[1]);
			weapon_shovel.SetWeaponTier(equipment_load[2]);
			weapon_rake.SetWeaponTier(equipment_load[3]);

			switch(equipment_load[7]) {
				case 0:
					active_weapon = weapon_knife;
					break;
				case 1:
					active_weapon = weapon_bat;
					break;
				case 2:
					active_weapon = weapon_shovel;
					break;
				case 3:
					active_weapon = weapon_rake;
					break;
				default:
					active_weapon = null;
					Debug.LogError("EQUIPMENT_MANAGER::START::No Active Weapon in Save File!");
					break;
			}
			if(active_weapon != null) {
				active_weapon.gameObject.SetActive(true);
				manager_player.EquipWeapon(active_weapon);
			}

			// Armour Loading
			armour_light.SetArmourTier(equipment_load[4]);
			armour_medium.SetArmourTier(equipment_load[5]);
			armour_heavy.SetArmourTier(equipment_load[6]);

			switch(equipment_load[8]) {
				case 0:
					active_armour = armour_light;
					break;
				case 1:
					active_armour = armour_medium;
					break;
				case 2:
					active_armour = armour_heavy;
					break;
				default:
					active_armour = null;
					Debug.LogError("EQUIPMENT_MANAGER::START::No Active Armour in PlayerPrefs!");
					break;
			}
			if(active_armour != null) {
				active_armour.gameObject.SetActive(true);
				manager_player.EquipArmour(active_armour);
			}
		}
		else {
			// Equip Initial Weapon
			active_weapon = weapon_bat;
			active_weapon.gameObject.SetActive(true);
			manager_player.EquipWeapon(active_weapon);

			// Equip Initial Armour
			active_armour = armour_light;
			active_armour.gameObject.SetActive(false);
			manager_player.EquipArmour(active_armour);
		}
		
	}

	public Weapon GetEquippedWeapon() {
		return active_weapon;
	}

	public Armour GetEquippedArmour() {
		return active_armour;
	}

	public void EquipWeapon(string item) {
		if(item.Equals(weapon_knife.GetWeaponName())) {
			SetActiveWeapon(weapon_knife);
			
		}
		else if(item.Equals(weapon_bat.GetWeaponName())) {
			SetActiveWeapon(weapon_bat);
		}
		else if(item.Equals(weapon_shovel.GetWeaponName())) {
			SetActiveWeapon(weapon_shovel);
		}
		else if(item.Equals(weapon_rake.GetWeaponName())) {
			SetActiveWeapon(weapon_rake);
		}
		else {
			Debug.Log("Unable to equip passed weapon");
		}
		
	}
	public void EquipArmour(string item) {
		if (item.Equals(armour_light.GetArmourName())) {
			SetActiveArmour(armour_light);
		}
		else if(item.Equals(armour_medium.GetArmourName())) {
			SetActiveArmour(armour_medium);
		}
		else if(item.Equals(armour_heavy.GetArmourName())) {
			SetActiveArmour(armour_heavy);
		}
		else {
			Debug.Log("Unable to equip passed armour");
		}

	}

	private void SetActiveWeapon(Weapon w) {
		// Deactivate Current Weapon
		active_weapon.gameObject.SetActive(false);
		// Set Current Weapon and Activate
		active_weapon = w;
		active_weapon.gameObject.SetActive(true);
	}

	private void SetActiveArmour(Armour a) {
		// Deactivate Current Armour
		active_armour.gameObject.SetActive(false);
		// Set Current Armour and Activate
		active_armour = a;
		active_armour.gameObject.SetActive(true);
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

	public int GetWeaponDamage(string weaponName)
	{
		if (weaponName == "Knife")
		{
			return (int)weapon_knife.GetDamage();
		}
		else if (weaponName == "Bat")
		{
			return (int)weapon_bat.GetDamage();
		}
		else if (weaponName == "Shovel")
		{
			return (int)weapon_shovel.GetDamage();
		}
		else if (weaponName == "Rake")
		{
			return (int)weapon_rake.GetDamage();
		}

		return -1;
	}

	public int GetArmourDefense(string armourName)
	{
		if (armourName == "Light Armour")
		{
			return (int)armour_light.GetDefense();
		}
		else if (armourName == "Medium Armour")
		{
			return (int)armour_medium.GetDefense();
		}
		else if (armourName == "Heavy Armour")
		{
			return (int)armour_heavy.GetDefense();
		}

		return -1;
	}

	public List<int> SaveEquipment() {
		List<int> equipment_save = new List<int>();

		// Weapons
		equipment_save.Add(weapon_knife.GetWeaponTier());
		equipment_save.Add(weapon_bat.GetWeaponTier());
		equipment_save.Add(weapon_shovel.GetWeaponTier());
		equipment_save.Add(weapon_rake.GetWeaponTier());

		// Armour
		equipment_save.Add(armour_light.GetArmourTier());
		equipment_save.Add(armour_medium.GetArmourTier());
		equipment_save.Add(armour_heavy.GetArmourTier());

		// Equipped Weapon
		switch (active_weapon.GetWeaponName()) {
			case "Knife":
				equipment_save.Add(0);
				break;
			case "Bat":
				equipment_save.Add(1);
				break;
			case "Shovel":
				equipment_save.Add(2);
				break;
			case "Rake":
				equipment_save.Add(3);
				break;
			default:
				equipment_save.Add(-1);
				break;

		}
		// Equipped Armour
		if (active_armour != null) {
			switch (active_armour.GetArmourName()) {
				case "Light Armour":
					equipment_save.Add(0);
					break;
				case "Medium Armour":
					equipment_save.Add(1);
					break;
				case "Heavy Armour":
					equipment_save.Add(2);
					break;
				default:
					equipment_save.Add(-1);
					break;
			}
		}
		else {
			equipment_save.Add(-1);
		}

		return equipment_save;
	}
}
