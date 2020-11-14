﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining player specific behaviour and statistics
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Player : Entity {

	// Component References
	[SerializeField] Camera player_cam;

	// Manager Class References
	[SerializeField] PlayerManager manager_stats;

	//[SerializeField] Inventory player_inventory;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Vector2 direction = player_cam.ScreenToWorldPoint(Input.mousePosition);
		direction = (direction - (Vector2)gameObject.transform.position).normalized;
		gameObject.transform.up = direction;

		// TODO: Cooldown for attack per update cycle

	}

	/*
	public void EquipWeapon(Weapon w) {
		UnequipWeapon();
		//player_stats.SetEquippedWeapon(w);
		//player_inventory.RemoveFromInventory(w);
	}

	public void EquipArmour(Armour a) {
		UnequipArmour();
		//player_stats.SetEquippedArmour(a);
		player_inventory.RemoveFromInventory(a);
	}

	private void UnequipWeapon() {
		//player_inventory.AddToInventory(weapon);
	}

	public void UnequipArmour() {
		player_inventory.AddToInventory(armour);
	}


	// TODO: Get all stats at once instead of one at a time for UI
	// Stat Grabbers
	public int GetCurrentHealth() {	return player_stats.GetCurrentHealth();	}
	public int GetMaxHealth() { return player_stats.GetMaxHealth(); }
	public int GetCurrentStamina() { return player_stats.GetCurrentStamina(); }
	public int GetMaxStamina() { return player_stats.GetMaxStamina(); }
	public int GetCurrentExperience() { return player_stats.GetCurrentExperience(); }
	public int GetCurrentNextLevel() { return player_stats.GetCurrentNextLevel(); }
	public int GetPlayerLevel() { return player_stats.GetCurrentLevel(); }
	public float GetCarryWeight() { return player_stats.GetCarryWeight(); }

	public Stats GetStats() { return player_stats;  }
	*/
}
