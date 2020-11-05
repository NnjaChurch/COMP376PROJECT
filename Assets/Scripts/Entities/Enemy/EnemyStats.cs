using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : Stats {
	// Level Scaling
	[SerializeField] int health_per_level = 10;
	[SerializeField] int damage_per_level = 5;
	
	private void CalculateEnemyStats() {
		max_health = BASE_HEALTH + (current_level * health_per_level);
		current_health = max_health;
		damage = BASE_DAMAGE + (current_level * damage_per_level);
		attack_speed = BASE_ATTACK_SPEED / equipped_weapon.GetSpeedModifier();
		movement_speed = BASE_MOVEMENT_SPEED / equipped_armour.GetMovementModifier();
	}
}
