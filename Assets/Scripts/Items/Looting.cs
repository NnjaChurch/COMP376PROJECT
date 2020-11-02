using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for object looting and random item generation
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------


public class Looting : MonoBehaviour {
	LootTable bag;
	LootTable furniture;
	LootTable vehicle;


	// Start is called before the first frame update
	void Start() {
		// loot table ranges for consumables, materials, weapons and armor
		// TODO: tweak these ranges

		//bag
		RangeInt c = new RangeInt(1, 25);
		RangeInt m = new RangeInt(26, 50);
		RangeInt w = new RangeInt(51, 75);
		RangeInt a = new RangeInt(76, 100);		
		bag = new LootTable(c, m, w, a);

		//furniture
		c = new RangeInt(1, 25);
		m = new RangeInt(26, 50);
		w = new RangeInt(51, 75);
		a = new RangeInt(76, 100);
		furniture = new LootTable(c, m, w, a);

		//vehicle
		c = new RangeInt(1, 25);
		m = new RangeInt(26, 50);
		w = new RangeInt(51, 75);
		a = new RangeInt(76, 100);
		vehicle = new LootTable(c, m, w, a);
	}

	// Update is called once per frame
	void Update() {

	}

}

public class LootTable
{
	RangeInt consumableRange;
	RangeInt materialRange;
	RangeInt weaponRange;
	RangeInt armourRange;

	public LootTable(RangeInt c, RangeInt m, RangeInt w, RangeInt a)
	{
		consumableRange = c;
		materialRange = m;
		weaponRange = w;
		armourRange = a;
	}
};