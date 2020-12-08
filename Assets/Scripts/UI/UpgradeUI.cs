using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the weapon and armor upgrade User Interface
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class UpgradeUI : MonoBehaviour {

	[SerializeField] UIManager manager_ui;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

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
}
