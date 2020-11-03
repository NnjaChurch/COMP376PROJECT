using System.Collections;
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
	public Stats player_stats;
	// Start is called before the first frame update
	void Start() 
	{
		player_stats.SetEquippedWeapon(weapon);
		player_stats.SetEquippedArmour(armour);
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
		player_stats.SetEquippedWeapon(weapon);
	}

	private void UnequipWeapon()
    {
		inventory.AddToInventory(weapon);
    }

	public void EquipArmour(Armour a)
    {
		UnequipArmour();
		armour = a;
		player_stats.SetEquippedArmour(armour);
    }

	public void UnequipArmour()
	{
		inventory.AddToInventory(armour);
	}

	public float GetPlayerCarryWeight() { return player_stats.GetCarryWeight(); }

	public float GetPlayerCurrentHealth() { return player_stats.GetCurrentHealth(); }

	public float GetPlayerBaseHealth() { return player_stats.GetBaseHealth(); }

	public float GetPlayerCurrentStamina() { return player_stats.GetCurrentStamina(); }

	public float GetPlayerBaseStamina() { return player_stats.GetBaseStamina(); }

	public float GetPlayerCurrentExperience() { return player_stats.GetCurrentExperience(); }

	public float GetPlayerBaseExperience() { return player_stats.GetBaseExperience(); }

	public float GetPlayerLevel() { return player_stats.GetPlayerLevel(); }
}
