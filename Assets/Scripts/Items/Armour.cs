using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : MonoBehaviour {

	// Attributes
	string armour_name;
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
}
