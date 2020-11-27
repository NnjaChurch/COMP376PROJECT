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

	// Start is called before the first frame update
	void Start() {
		upgrade_tier = 0;
		CalculateDefense();
	}

	public void UpgradeArmour() {
		upgrade_tier++;
		CalculateDefense();
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

	public void SetArmourTier(int tier) {
		upgrade_tier = tier;
		CalculateDefense();
	}

	private void CalculateDefense() {
		defense = BASE_DEFENSE + (upgrade_tier * DEFENSE_SCALING);
	}
}
