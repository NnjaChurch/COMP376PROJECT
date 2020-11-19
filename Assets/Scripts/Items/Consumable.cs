using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for items of the type consumable, their properties, and their interactions
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Consumable : Item {

	[SerializeField] string consumable_name;
	string description;
	[SerializeField] int hp_gain;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public int GetHPGain() { return hp_gain; }

	public string GetConsumableName() { return consumable_name; }
}
