using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the weapon and armor upgrade User Interface
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class UpgradeUI : MonoBehaviour {

	[SerializeField] UIManager manager_ui;

	[SerializeField] GameObject upgrade_content; // To get reference to each weapon/armour display panel
	[SerializeField] Text[] total_materials;

	IDictionary<string, int> weaponDictionary;
	IDictionary<string, int> armourDictionary;

	public void Initialize() {
		Debug.Log("Initializing UpgradeUI...");

		weaponDictionary = manager_ui.GetFoundWeapons();
		armourDictionary = manager_ui.GetFoundArmour();
		UpdateUpgradeUI();
	}

	private void GetEquipmentInformation() {
		List<int> bat_info = manager_ui.GetWeaponInfo("Bat");
		List<int> knife_info = manager_ui.GetWeaponInfo("Knife");
		List<int> shovel_info = manager_ui.GetWeaponInfo("Shovel");
		List<int> rake_info = manager_ui.GetWeaponInfo("Rake");

		List<int> light_info = manager_ui.GetArmourInfo("Light Armour");
		List<int> medium_info = manager_ui.GetArmourInfo("Medium Armour");
		List<int> heavy_info = manager_ui.GetArmourInfo("Heavy Armour");
	}
	private void GetMaterialInformation() {
		// TODO: Update Material Tags Stored in Variables for now
		List<int> bat_materials = manager_ui.GetWeaponMaterials("Bat");
		List<int> knife_materials = manager_ui.GetWeaponMaterials("Knife");
		List<int> shovel_materials = manager_ui.GetWeaponMaterials("Shovel");
		List<int> rake_materials = manager_ui.GetWeaponMaterials("Rake");

		List<int> light_materials = manager_ui.GetArmourMaterials("Light Armour");
		List<int> medium_materials = manager_ui.GetArmourMaterials("Medium Armour");
		List<int> heavy_materials = manager_ui.GetArmourMaterials("Heavy Armour");
	}

	public void UpdateUpgradeUI()
    {
		Transform itemSlot;

		foreach (KeyValuePair<string, int> entry in weaponDictionary)
		{
			itemSlot = upgrade_content.transform.Find(entry.Key);
			itemSlot.gameObject.SetActive(true);

			List<int> weaponInfo = manager_ui.GetWeaponInfo(entry.Key);
			//GetWeaponInfo returns new List<int> { upgrade_tier, damage, DAMAGE_SCALING };

			itemSlot.Find("WeaponDamage").GetComponent<Text>().text = "Damage: " + weaponInfo[1];
			itemSlot.Find("WeaponTier").GetComponent<Text>().text = "Tier: " + weaponInfo[0];

			List<int> weaponMaterials = manager_ui.GetWeaponMaterials(entry.Key);
			// GetWeaponMaterials returns new List<int> { upgrade_nails, upgrade_wood, 0, 0 };

			itemSlot.Find("Nails").Find("Text").GetComponent<Text>().text = "" + weaponMaterials[0];
			itemSlot.Find("Wood").Find("Text").GetComponent<Text>().text = "" + weaponMaterials[1];
		}

		foreach (KeyValuePair<string, int> entry in armourDictionary)
		{
			itemSlot = upgrade_content.transform.Find(entry.Key);
			itemSlot.gameObject.SetActive(true);

			List<int> armourInfo = manager_ui.GetArmourInfo(entry.Key);
			//GetArmourInfo returns new List<int> { upgrade_tier, defense, DEFENSE_SCALING };

			itemSlot.Find("ArmourDefense").GetComponent<Text>().text = "Defense: " + armourInfo[1];
			itemSlot.Find("ArmourTier").GetComponent<Text>().text = "Tier: " + armourInfo[0];

			List<int> armourMaterials = manager_ui.GetArmourMaterials(entry.Key);
			// GetArmourMaterials returns new List<int> { 0, 0, upgrade_metal, upgrade_cloth };

			itemSlot.Find("Metal").Find("Text").GetComponent<Text>().text = "" + armourMaterials[2];
			itemSlot.Find("Cloth").Find("Text").GetComponent<Text>().text = "" + armourMaterials[3];
		}

		IDictionary<string, int> materials = manager_ui.GetMaterials();
		int index = 0;

		foreach (KeyValuePair<string, int> entry in materials)
        {
			total_materials[index].text = "" + entry.Value;
			index++;
        }
	}

	public void UpdateWeapon(string weapon_name)
    {
		manager_ui.UpgradeWeapon(weapon_name);
		UpdateUpgradeUI();
    }

	public void UpdateArmour(string armour_name)
    {
		manager_ui.UpgradeArmour(armour_name);
		UpdateUpgradeUI();
	}
}
