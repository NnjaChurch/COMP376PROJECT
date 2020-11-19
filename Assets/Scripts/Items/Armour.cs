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
	[SerializeField] int defense;
	[SerializeField] int movement_modifier;
	[SerializeField] int DEFENSE_SCALING;
	int upgrade_tier;

	// Start is called before the first frame update
	void Start() {
		upgrade_tier = 0;
	}

	// Update is called once per frame
	void Update() {

	}

	public void UpgradeArmour() {
		upgrade_tier++;
		defense += DEFENSE_SCALING;
	}

	public float GetMovementModifier() {
		return movement_modifier;
	}

	public int GetDefense() {
		return defense;
	}

	public string GetArmourName() { return armour_name; }
}
