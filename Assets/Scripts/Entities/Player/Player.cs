using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining player specific behaviour and statistics
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Player : Entity {
	public Camera player_cam;
	public Inventory inventory;
	public PlayerStats player_stats;
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

	public void EquipWeapon(Weapon w) {
		UnequipWeapon();
		player_stats.SetEquippedWeapon(w);
	}

	public void EquipArmour(Armour a) {
		UnequipArmour();
		player_stats.SetEquippedArmour(a);
	}

	private void UnequipWeapon() {
		inventory.AddToInventory(weapon);
	}

	public void UnequipArmour() {
		inventory.AddToInventory(armour);
	}
}
