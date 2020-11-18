using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class to calculate and keep track of player stats
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class PlayerStats : Stats {

	// Persistent Attributes (must carry across scenes)
	int strength;
	int dexterity;
	int intelligence;

	int current_experience;
	int current_next_level;
	int stat_points;
	int skill_points;


	// Player Specific Stats
	[SerializeField] int BASE_STAMINA = 100;
	[SerializeField] int BASE_CARRY_WEIGHT = 50;        // lbs
	[SerializeField] int BASE_EXPERIENCE_GAIN = 1;      // %
	[SerializeField] int BASE_HEALING_EFFICACY = 1;     // %
	[SerializeField] int BASE_DAMAGE_REDUCTION = 1;     // %
	[SerializeField] int BASE_EXPERIENCE_LEVEL = 100;   // %
	[SerializeField] float EXPERIENCE_GROWTH = 1.2f;

	// Calculated Stats
	int max_stamina;
	float carry_weight;
	float experience_gain;
	float healing_efficacy;
	float damage_reduction;
	float encumbered_modifier;

	// Current Stats
	int current_stamina;

	// Manager Reference
	[SerializeField] PlayerManager manager_stats;

	private void Start() {
		CalculatePlayerStats();
		FullHeal();
	}

	
	private void CalculatePlayerStats() {
		// Strength Stats
		max_health = Mathf.FloorToInt((BASE_HEALTH + (10 * strength)) * manager_stats.GetSkillBonus(0));
		damage = Mathf.FloorToInt((BASE_DAMAGE + strength) * manager_stats.GetSkillBonus(1));
		carry_weight = (BASE_CARRY_WEIGHT + (2 * strength)) * manager_stats.GetSkillBonus(2);

		// Dexterity Stats
		max_stamina = Mathf.FloorToInt((BASE_STAMINA + (10 * dexterity)) * manager_stats.GetSkillBonus(3));
		movement_speed = (((BASE_MOVEMENT_SPEED + (0.05f * dexterity)) * (equipped_armour ? equipped_armour.GetMovementModifier() : 1.0f)) * manager_stats.GetSkillBonus(4)) / encumbered_modifier ;
		attack_speed = (BASE_ATTACK_SPEED / (equipped_weapon ? equipped_weapon.GetSpeedModifier() : 1.0f)) / manager_stats.GetSkillBonus(5);

		// Intelligence Stats
		experience_gain = (BASE_EXPERIENCE_GAIN + manager_stats.GetSkillBonus(6));
		healing_efficacy = (BASE_HEALING_EFFICACY + manager_stats.GetSkillBonus(7));
		damage_reduction = (BASE_DAMAGE_REDUCTION + manager_stats.GetSkillBonus(8));
	}
	

	private void FullHeal() {
		current_stamina = max_stamina;
		current_health = max_health;
	}

	// TODO: Experience and Level Functions

	public int GetCurrentStamina() { return current_stamina; }
	public int GetMaxStamina() { return max_stamina; }
	public float GetCarryWeight() { return carry_weight; }
	public int GetCurrentExperience() { return current_experience; }
	public int GetCurrentNextLevel() { return current_next_level; }
	public int GetStrength() { return strength; }
	public int GetDexterity() { return dexterity; }
	public int GetIntelligence() { return intelligence; }
	public void SetEquippedWeapon(Weapon w) {
		equipped_weapon = w;
		CalculatePlayerStats();
	}

	public void SetEquippedArmour(Armour a) {
		equipped_armour = a;
		CalculatePlayerStats();
	}
}
