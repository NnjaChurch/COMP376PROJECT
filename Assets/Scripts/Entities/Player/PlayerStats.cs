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
	[SerializeField] int BASE_CARRY_WEIGHT = 50;		// lbs
	[SerializeField] int BASE_EXPERIENCE_GAIN = 1;		// %
	[SerializeField] int BASE_HEALING_EFFICACY = 1;		// %
	[SerializeField] int BASE_DAMAGE_REDUCTION = 1;		// %
	[SerializeField] int BASE_EXPERIENCE_LEVEL = 100;	// %
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

	// Skills
	List<Skill> skill_list;
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

	private void CalculatePlayerStats() {
		// Strength Stats
		max_health = Mathf.FloorToInt((BASE_HEALTH + (10 * strength)) * skill_list[0].GetBonus());
		damage = (BASE_DAMAGE + strength);
		carry_weight = (BASE_CARRY_WEIGHT + (2 * strength)) * skill_list[2].GetBonus();

		// Dexterity Stats
		max_stamina = Mathf.FloorToInt((BASE_STAMINA + (10 * dexterity)) * skill_list[3].GetBonus());
		attack_speed = (BASE_ATTACK_SPEED / (equipped_weapon ? equipped_weapon.GetSpeedModifier() : 1.0f)) / skill_list[5].GetBonus();
		movement_speed = ((BASE_MOVEMENT_SPEED + (5 * dexterity)) * (equipped_armour ? equipped_armour.GetMovementModifier() : 1.0f)) / encumbered_modifier;

		// Intelligence Stats
		experience_gain = (BASE_EXPERIENCE_GAIN + skill_list[6].GetBonus());
		healing_efficacy = (BASE_HEALING_EFFICACY + skill_list[7].GetBonus());
		damage_reduction = (BASE_DAMAGE_REDUCTION + skill_list[8].GetBonus());
	}

	private void FullHeal() {
		current_stamina = max_stamina;
		current_health = max_health;
	}

	// TODO: Experience and Level Functions

	public float GetMaxStamina() { return max_stamina; }
	public float GetCarryWeight() { return carry_weight; }
	public float GetCurrentStamina() { return current_stamina; }
	public float GetCurrentExperience() { return current_experience; }
	public float GetCurrentNextLevel() { return current_next_level; }
	public void SetEquippedWeapon(Weapon w) {
		equipped_weapon = w;
		CalculatePlayerStats();
	}

	public void SetEquippedArmour(Armour a) {
		equipped_armour = a;
		CalculatePlayerStats();
	}
}
