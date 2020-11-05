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
	public string armour_name;
	int upgrade_tier;
	float movement_modifier;
	int defense;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public float GetMovementModifier() {
		return movement_modifier;
	}

	public int GetDefense() {
		return defense;
	}

	public string GetArmourName() { return armour_name; }
}
