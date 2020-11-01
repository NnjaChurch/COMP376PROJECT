using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

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
	const int BASE_ATTACK_SPEED = 2;		// seconds
	const int BASE_MOVEMENT_SPEED = 1;		// %
	const int BASE_CARRY_WEIGHT = 50;		// lbs
	const int BASE_EXPERIENCE_GAIN = 1;		// %
	const int BASE_HEALING_EFFICACY = 1;	// %
	const int BASE_DAMAGE_TAKEN = 1;        // %

	const int BASE_EXPERIENCE_LEVEL = 100;
	const float EXPERIENCE_GROWTH = 1.2f;

	// Calculated Stats
	int max_health;
	int max_stamina;
	int attack_speed;
	int movement_speed;
	int carry_weight;
	int experience_gain;
	int healing_efficacy;
	int damage_taken;

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
		max_health = Mathf.FloorToInt((BASE_HEALTH + (10 * strength)) * skill_list[0].GetBonus());
		current_health = max_health;
		max_stamina = Mathf.FloorToInt((BASE_STAMINA + (10 * dexterity)) * skill_list[3].GetBonus());
		current_stamina = max_stamina;

	}
}
