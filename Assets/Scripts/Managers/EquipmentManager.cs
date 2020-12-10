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
	[SerializeField] LootManager manager_loot;

	// Equipped Item References
	Weapon active_weapon;
	Armour active_armour;

	public bool Initialize() {
		Debug.Log("Initializing EquipmentManager...");
		if (manager_save.CheckSave()) {
			List<int> equipment_load = manager_save.LoadEquipment();

			// Weapon Loading
			weapon_knife.SetWeaponTier(equipment_load[0]);
			weapon_bat.SetWeaponTier(equipment_load[1]);
			weapon_shovel.SetWeaponTier(equipment_load[2]);
			weapon_rake.SetWeaponTier(equipment_load[3]);

			switch (equipment_load[7]) {
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
			if (active_weapon != null) {
				active_weapon.gameObject.SetActive(true);
				manager_player.EquipWeapon(active_weapon);
				manager_loot.WeaponFound(active_weapon.GetWeaponName());
			}

			// Armour Loading
			armour_light.SetArmourTier(equipment_load[4]);
			armour_medium.SetArmourTier(equipment_load[5]);
			armour_heavy.SetArmourTier(equipment_load[6]);

			switch (equipment_load[8]) {
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
			if (active_armour != null) {
				active_armour.gameObject.SetActive(true);
				manager_player.EquipArmour(active_armour);
				manager_loot.ArmourFound(active_armour.GetArmourName());
			}
			return true;
		}
		else {
			// Equip Initial Weapon
			active_weapon = weapon_bat;
			active_weapon.gameObject.SetActive(true);
			manager_player.EquipWeapon(active_weapon);
			manager_loot.WeaponFound(active_weapon.name);

			// Equip Initial Armour
			active_armour = armour_light;
			active_armour.gameObject.SetActive(true);
			manager_player.EquipArmour(active_armour);

			manager_loot.ArmourFound(active_armour.name);
			return false;

		}

	}

	public Weapon GetEquippedWeapon() {
		return active_weapon;
	}

	public Armour GetEquippedArmour() {
		return active_armour;
	}

	public void EquipWeapon(string item) {
		if (item.Equals(weapon_knife.GetWeaponName())) {
			SetActiveWeapon(weapon_knife);

		}
		else if (item.Equals(weapon_bat.GetWeaponName())) {
			SetActiveWeapon(weapon_bat);
		}
		else if (item.Equals(weapon_shovel.GetWeaponName())) {
			SetActiveWeapon(weapon_shovel);
		}
		else if (item.Equals(weapon_rake.GetWeaponName())) {
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
		else if (item.Equals(armour_medium.GetArmourName())) {
			SetActiveArmour(armour_medium);
		}
		else if (item.Equals(armour_heavy.GetArmourName())) {
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

	public int GetWeaponWeight(string weapon_name) {
		if (weapon_name == "Knife") {
			return (int)weapon_knife.GetWeight();
		}
		else if (weapon_name == "Bat") {
			return (int)weapon_bat.GetWeight();
		}
		else if (weapon_name == "Shovel") {
			return (int)weapon_shovel.GetWeight();
		}
		else if (weapon_name == "Rake") {
			return (int)weapon_rake.GetWeight();
		}
		return -1;
	}

	public int GetArmourWeight(string armour_name) {
		if (armour_name == "Light Armour") {
			return (int)armour_light.GetWeight();
		}
		else if (armour_name == "Medium Armour") {
			return (int)armour_medium.GetWeight();
		}
		else if (armour_name == "Heavy Armour") {
			return (int)armour_heavy.GetWeight();
		}
		return -1;
	}

	public int GetWeaponDamage(string weapon_name) {
		if (weapon_name == "Knife") {
			return (int)weapon_knife.GetDamage();
		}
		else if (weapon_name == "Bat") {
			return (int)weapon_bat.GetDamage();
		}
		else if (weapon_name == "Shovel") {
			return (int)weapon_shovel.GetDamage();
		}
		else if (weapon_name == "Rake") {
			return (int)weapon_rake.GetDamage();
		}
		return -1;
	}

	public int GetArmourDefense(string armour_name) {
		if (armour_name == "Light Armour") {
			return (int)armour_light.GetDefense();
		}
		else if (armour_name == "Medium Armour") {
			return (int)armour_medium.GetDefense();
		}
		else if (armour_name == "Heavy Armour") {
			return (int)armour_heavy.GetDefense();
		}
		return -1;
	}

	public List<int> GetWeaponMaterials(string weapon_name) {
		if (weapon_name == "Knife") {
			return weapon_knife.GetMaterialCost();
		}
		else if (weapon_name == "Bat") {
			return weapon_bat.GetMaterialCost();
		}
		else if (weapon_name == "Shovel") {
			return weapon_shovel.GetMaterialCost();
		}
		else if (weapon_name == "Rake") {
			return weapon_rake.GetMaterialCost();
		}
		return new List<int>(4);
	}

	public List<int> GetArmourMaterials(string armour_name) {
		if (armour_name == "Light Armour") {
			return armour_light.GetMaterialCost();
		}
		else if (armour_name == "Medium Armour") {
			return armour_medium.GetMaterialCost();
		}
		else if (armour_name == "Heavy Armour") {
			return armour_heavy.GetMaterialCost();
		}
		return new List<int>(4);
	}

	public int GetWeaponTier(string weapon_name) {
		if (weapon_name == "Knife") {
			return weapon_knife.GetWeaponTier();
		}
		else if (weapon_name == "Bat") {
			return weapon_bat.GetWeaponTier();
		}
		else if (weapon_name == "Shovel") {
			return weapon_shovel.GetWeaponTier();
		}
		else if (weapon_name == "Rake") {
			return weapon_rake.GetWeaponTier();
		}
		return -1;
	}

	public int GetArmourTier(string armour_name) {
		if (armour_name == "Light Armour") {
			return armour_light.GetArmourTier();
		}
		else if (armour_name == "Medium Armour") {
			return armour_medium.GetArmourTier();
		}
		else if (armour_name == "Heavy Armour") {
			return armour_heavy.GetArmourTier();
		}
		return -1;
	}

	public List<int> GetWeaponInfo(string weapon_name) {
		// Returns List with Entries { upgrade_tier, damage, damage_scaling }
		if (weapon_name == "Knife") {
			return weapon_knife.GetWeaponInfo();
		}
		else if (weapon_name == "Bat") {
			return weapon_bat.GetWeaponInfo();
		}
		else if (weapon_name == "Shovel") {
			return weapon_shovel.GetWeaponInfo();
		}
		else if (weapon_name == "Rake") {
			return weapon_rake.GetWeaponInfo();
		}
		return new List<int>(3);
	}

	public List<int> GetArmourInfo(string armour_name) {
		if (armour_name == "Light Armour") {
			return armour_light.GetArmourInfo();
		}
		else if (armour_name == "Medium Armour") {
			return armour_medium.GetArmourInfo();
		}
		else if (armour_name == "Heavy Armour") {
			return armour_heavy.GetArmourInfo();
		}


		return new List<int>(3);
	}

	public bool UpgradeWeapon(string weapon_name) {
		if (weapon_name == "Knife") {
			return weapon_knife.UpgradeWeapon();
		}
		else if (weapon_name == "Bat") {
			return weapon_bat.UpgradeWeapon();
		}
		else if (weapon_name == "Shovel") {
			return weapon_shovel.UpgradeWeapon();
		}
		else if (weapon_name == "Rake") {
			return weapon_rake.UpgradeWeapon();
		}
		return false;
	}

	public bool UpgradeArmour(string armour_name) {
		if (armour_name == "Light Armour") {
			return armour_light.UpgradeArmour();
		}
		else if (armour_name == "Medium Armour") {
			return armour_medium.UpgradeArmour();
		}
		else if (armour_name == "Heavy Armour") {
			return armour_heavy.UpgradeArmour();
		}
		return false;
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
