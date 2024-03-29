﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats {
	// Level Scaling
	[SerializeField] int health_per_level = 10;
	[SerializeField] int damage_per_level = 5;
	[SerializeField] Weapon initial_weapon;
	[SerializeField] Armour initial_armour;
	[SerializeField] Enemy enemy;
	[SerializeField] AudioSource[] audioTakeDamage;
	[SerializeField] int experienceReward;

	private void Start() {
		equipped_weapon = initial_weapon;
		equipped_armour = initial_armour;
		CalculateEnemyStats();
		attack_timer = attack_speed;
		canAttack = true;
		isAttacking = false;
	}

	private void Update() {
		if (isAttacking) {
			attack_timer -= Time.deltaTime;
			if (attack_timer < 0) {
				attack_timer = 0;
			}
			if (attack_timer == 0) {
				Attack();
			}
		}

	}
	public void StartAttack() {
		if (canAttack && !isAttacking) {
			attack_timer = attack_speed;
			canAttack = false;
			isAttacking = true;
		}
		else {
			//print("The enemy can't attack right now.");
		}
	}
	private void Attack() {
		equipped_weapon.UseWeapon(damage, this);
		isAttacking = false;
		canAttack = true;
	}
	private void CalculateEnemyStats() {
		max_health = BASE_HEALTH + (current_level * health_per_level);
		current_health = max_health;
		damage = BASE_DAMAGE + (current_level * damage_per_level);
		attack_speed = BASE_ATTACK_SPEED / equipped_weapon.GetSpeedModifier();
		movement_speed = BASE_MOVEMENT_SPEED / equipped_armour.GetMovementModifier();
	}

	public int TakeDamage(int damage) {
		int taken_damage = Mathf.FloorToInt(damage / ((float)equipped_armour.GetDefense() / 100));
		//Debug.Log("Damage Taken: " + taken_damage);
		current_health -= taken_damage;
		if (current_health <= 0) {
			current_health = 0;
			enemy.Kill();
		}
		else {
			audioTakeDamage[Random.Range(0, audioTakeDamage.Length)].Play();
		}
		return current_health;
	}

	public int GetExpReward() {
		return experienceReward; //TODO !
	}
}
