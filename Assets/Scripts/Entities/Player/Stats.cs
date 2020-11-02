using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class to calculate and keep track of player stats
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Stats : MonoBehaviour {

	// TODO: Implement Serializability for Stats to allow transfer over zones
	// TODO: Implement file loading for skills and stats to allow easy modification outside of code.

	// References

	// Persistent Attributes (must carry across scenes)
	int strength;
	int dexterity;
	int intelligence;

	int player_level;
	int current_experience;
	int current_next_level;
	int stat_points;
	int skill_points;

	// Current Stats
	int current_health;
	int current_stamina;

	// Base Stats
	const int BASE_HEALTH = 100;
	const int BASE_STAMINA = 100;
	const int BASE_DAMAGE = 5;
	const int BASE_ATTACK_SPEED = 2;        // seconds
	const int BASE_MOVEMENT_SPEED = 1;      // %
	const int BASE_CARRY_WEIGHT = 50;       // lbs
	const int BASE_EXPERIENCE_GAIN = 1;     // %
	const int BASE_HEALING_EFFICACY = 1;    // %
	const int BASE_DAMAGE_REDUCTION = 1;    // %

	const int BASE_EXPERIENCE_LEVEL = 100;
	const float EXPERIENCE_GROWTH = 1.2f;

	// Calculated Stats
	int max_health;
	int max_stamina;
	int damage;
	float attack_speed;
	float movement_speed;
	float carry_weight;
	float experience_gain;
	float healing_efficacy;
	float damage_reduction;
	float encumbered_modifier;

	// Skills
	List<Skill> skill_list;

	// Equipment
	Weapon equipped_weapon;
	Armour equipped_armour;

	private void Start() {
		InitializeSkills();
		CalculateStats();
	}

	private void InitializeSkills() {
		// Initialize Skill List
		skill_list = new List<Skill>();
		skill_list.Add(new Skill("Health Boost", 20, 0.05f));
		skill_list.Add(new Skill("Damage Boost", 20, 0.10f));
		skill_list.Add(new Skill("Weight Boost", 10, 0.05f));
		skill_list.Add(new Skill("Stamina Boost", 20, 0.05f));
		skill_list.Add(new Skill("Movement Speed Boost", 10, 0.10f));
		skill_list.Add(new Skill("Attack Speed Boost", 20, 0.05f));
		skill_list.Add(new Skill("Experience Boost", 20, 0.05f));
		skill_list.Add(new Skill("Healing Boost", 10, 0.05f));
		skill_list.Add(new Skill("Damage Reduction Boost", 25, 0.02f));
	}
	private void CalculateStats() {     // Run when a zone is loaded
										// Strength Stats
		max_health = Mathf.FloorToInt((BASE_HEALTH + (10 * strength)) * skill_list[0].GetBonus());
		current_health = max_health;
		damage = (BASE_DAMAGE + strength);  // TODO: Incorporate Weapon Damage? Or Calculate Damage in Weapon?
		carry_weight = (BASE_CARRY_WEIGHT + (2 * strength)) * skill_list[2].GetBonus();

		// Dexterity Stats
		max_stamina = Mathf.FloorToInt((BASE_STAMINA + (10 * dexterity)) * skill_list[3].GetBonus());
		current_stamina = max_stamina;
		attack_speed = (BASE_ATTACK_SPEED / equipped_weapon.GetSpeedModifier()) / skill_list[5].GetBonus();
		movement_speed = ((BASE_MOVEMENT_SPEED + (5 * dexterity)) * equipped_armour.GetMovementModifier()) / encumbered_modifier;

		// Intelligence Stats
		experience_gain = (BASE_EXPERIENCE_GAIN + skill_list[6].GetBonus());
		healing_efficacy = (BASE_HEALING_EFFICACY + skill_list[7].GetBonus());
		damage_reduction = (BASE_DAMAGE_REDUCTION + skill_list[8].GetBonus());
	}

	// Getters

	float GetCarryWeight() { return carry_weight; }
}


/*
 * 1.0
 * 1.2
 * 0.8
 * 1.0
 */