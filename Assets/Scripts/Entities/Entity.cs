using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining basic entity behaviour and statistics
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public abstract class Entity : MonoBehaviour {

	public Weapon weapon;
	public Armour armour;
	// This is the time that the weapon will be available again
	float attack_cooldown;

	// Start is called before the first frame update
	void Start() {
		attack_cooldown = 0.0f;
	}

	// Update is called once per frame
	void Update() {

	}

	public int TakeDamage(float damage) {
		print("entity is taking damage!");
		return 0;
	}
	public Weapon GetWeapon() {
		return weapon;
	}

	public void Attack() {
		weapon.UseWeapon(0);	// <--- pass entity damage value?
	}
}
