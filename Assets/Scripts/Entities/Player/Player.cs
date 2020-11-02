﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining player specific behaviour and statistics
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Player : Entity
{
	public Camera player_cam;
	public Weapon weapon;
	public Armour armour;
	public Inventory inventory;
	// Start is called before the first frame update
	void Start() 
	{
	}

	// Update is called once per frame
	void Update() 
	{

		Vector2 direction = player_cam.ScreenToWorldPoint(Input.mousePosition);
		direction = (direction - (Vector2)gameObject.transform.position).normalized;
		gameObject.transform.up = direction;

	}

	public Weapon GetWeapon()
    {
		return weapon;
    }

	public void Attack() {
		// Do math with stats and pass appropriate values

		// Get damage and attack speed from Stats
	}

	// TODO: Add way to incorporate damage from stats into weapon damage when attacking
	public void EquipWeapon(Weapon w)
    {
		UnequipWeapon();
		weapon = w;
    }

	private void UnequipWeapon()
    {
		inventory.AddToInventory(weapon);
    }

	public void EquipArmour(Armour a)
    {
		UnequipArmour();
		armour = a;
    }

	public void UnequipArmour()
	{
		inventory.AddToInventory(armour);
	}
}
