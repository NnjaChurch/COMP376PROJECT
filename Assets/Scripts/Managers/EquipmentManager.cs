﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	// Weapon References
	[SerializeField] Weapon weapon_knife;
	[SerializeField] Weapon weapon_bat;
	[SerializeField] Weapon weapon_shovel;
	[SerializeField] Weapon weapon_rake;

	// Armour References
	[SerializeField] Armour armour_light;
	[SerializeField] Armour armour_medium;
	[SerializeField] Armour armour_heavy;

	// Manager Class References
	[SerializeField] PlayerManager manager_stats;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public Weapon GetEquippedWeapon()
    {
		//TODO
		return weapon_bat;
    }

	public Armour GetEquippedArmour()
    {
		//TODO
		return armour_light;
    }

	public void EquipWeapon(string item)
    {
		Debug.Log("equipping " + item);
    }
	public void EquipArmour(string item)
    {
		Debug.Log("equipping " + item);

	}
}
