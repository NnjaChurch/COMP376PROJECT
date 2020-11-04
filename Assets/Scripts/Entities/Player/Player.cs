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

	// TODO: Add way to incorporate damage from stats into weapon damage when attacking
	public void EquipWeapon(Weapon w)
	{
		UnequipWeapon();
		weapon = w;
		stats.SetEquippedWeapon(weapon);
	}

	public void EquipArmour(Armour a)
	{
		UnequipArmour();
		armour = a;
		stats.SetEquippedArmour(armour);
	}

	private void UnequipWeapon()
	{
		inventory.AddToInventory(weapon);
	}

	public void UnequipArmour()
	{
		inventory.AddToInventory(armour);
	}
}
