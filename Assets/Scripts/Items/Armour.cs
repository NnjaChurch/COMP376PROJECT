using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for items of the type armour, their properties and their interactions
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Armour : Item {

	// Attributes
	[SerializeField] string armour_name;
	[SerializeField] int BASE_DEFENSE;
	[SerializeField] int movement_modifier;
	[SerializeField] int DEFENSE_SCALING;

	int upgrade_tier;
	int defense;

	int upgrade_metal;
	int upgrade_cloth;

	// Start is called before the first frame update
	void Start() {
		upgrade_tier = 0;
		CalculateDefense();
		CalculateUpgradeMaterials();
	}

	public void UpgradeArmour() {
		upgrade_tier++;
		CalculateDefense();
		CalculateUpgradeMaterials();
	}

	public float GetMovementModifier() {
		return movement_modifier;
	}

	public int GetDefense() {
		return defense;
	}

	public string GetArmourName() {
		return armour_name;
	}

	public int GetArmourTier() {
		return upgrade_tier;
	}

	public int GetArmourScaling() {
		return DEFENSE_SCALING;
	}

	public List<int> GetArmourInfo() {
		// Returns list with armour info { upgrade_tier, defense, defense_scaling }
		return new List<int> { upgrade_tier, defense, DEFENSE_SCALING };
	}

	public List<int> GetMaterialCost() {
		List<int> material_list = new List<int> { 0, 0, upgrade_metal, upgrade_cloth };
		return material_list;
	}

	public void SetArmourTier(int tier) {
		upgrade_tier = tier;
		CalculateDefense();
		CalculateUpgradeMaterials();
	}

	private void CalculateDefense() {
		defense = BASE_DEFENSE + (upgrade_tier * DEFENSE_SCALING);
	}

	private void CalculateUpgradeMaterials() {
		upgrade_metal = 5 + (5 * upgrade_tier);
		upgrade_cloth = 5 + (5 * upgrade_tier);
	}
}
