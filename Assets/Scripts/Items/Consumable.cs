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
	[SerializeField] string description;
	[SerializeField] int hp_gain;

	public int GetHPGain() { return hp_gain; }
	public string GetConsumableName() { return consumable_name; }
	public string GetConsumableDescription() { return description; }
}
