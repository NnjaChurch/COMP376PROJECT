﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class to calculate and keep track of entity stats
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Stats : MonoBehaviour {

	// Entity Base Stats
	[SerializeField] protected int BASE_HEALTH = 100;
	[SerializeField] protected int BASE_DAMAGE = 5;
	[SerializeField] protected float BASE_ATTACK_SPEED = 2;       // Seconds
	[SerializeField] protected float BASE_MOVEMENT_SPEED = 1;     // %

	// Current Stats
	protected int current_level;
	protected int current_health;

	// Timer Stats
	protected float attack_timer;
	protected bool canAttack;
	protected bool isAttacking;

	// Calculated Stats
	protected int max_health;
	protected int damage;
	protected float attack_speed;
	protected float movement_speed;

	// Equipment
	protected Weapon equipped_weapon;
	protected Armour equipped_armour;

	// Getters
	public int GetCurrentLevel() { return current_level; }
	public int GetMaxHealth() { return max_health; }
	public int GetCurrentHealth() { return current_health; }
	public int GetDamage() { return damage; }
	public float GetAttackSpeed() { return attack_speed; }
	public float GetMovementSpeed() { return movement_speed; }
	public void SetLevel(int level)
    {
		current_level = level;
    }

}