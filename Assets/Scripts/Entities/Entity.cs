using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining basic entity behaviour and statistics
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Entity : MonoBehaviour {

	public Weapon weapon;
	public Armour armour;
	public Stats stats;
	// This is the time that the weapon will be available again
	float attack_cooldown;

	// Start is called before the first frame update
	void Start() {
		attack_cooldown = 0.0f;
		stats.SetEquippedWeapon(weapon);
		stats.SetEquippedArmour(armour);
	}

	// Update is called once per frame
	void Update() {
	}

	public int TakeDamage(float damage) {
		print("entity is taking damage!");
		return 0;
	}
	public Weapon GetWeapon()
	{
		return weapon;
	}


	public void Attack()
    {
		float time = Time.time;

		if (time < attack_cooldown)
        {
			attack_cooldown = time + stats.GetAttackSpeed();
			return;
        } else
        {
			weapon.UseWeapon();
        }
    }

	public float GetCarryWeight() { return stats.GetCarryWeight(); }

	public float GetCurrentHealth() { return stats.GetCurrentHealth(); }

	public float GetBaseHealth() { return stats.GetBaseHealth(); }

	public float GetCurrentStamina() { return stats.GetCurrentStamina(); }

	public float GetBaseStamina() { return stats.GetBaseStamina(); }

	public float GetCurrentExperience() { return stats.GetCurrentExperience(); }

	public float GetBaseExperience() { return stats.GetBaseExperience(); }

	public float GetLevel() { return stats.GetPlayerLevel(); }
}
