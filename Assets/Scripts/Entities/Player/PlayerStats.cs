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
	[SerializeField] PlayerManager manager_player;

	private void Start() {
		CalculatePlayerStats();
		FullHeal();
		attack_timer = attack_speed;
		canAttack = true;
		isAttacking = false;
	}
	private void Update() {
		// Attack Cooldown
		if(attack_timer > 0) {
			attack_timer -= Time.deltaTime;
			if(attack_timer < 0) {
				attack_timer = 0;
			}
			if (attack_timer == 0) {
				canAttack = true;
			}
		}
	}


	private void CalculatePlayerStats() {
		// Strength Stats
		max_health = Mathf.FloorToInt((BASE_HEALTH + (10 * strength)) * manager_player.GetSkillBonus(0));
		damage = Mathf.FloorToInt((BASE_DAMAGE + strength) * manager_player.GetSkillBonus(1));
		carry_weight = (BASE_CARRY_WEIGHT + (2 * strength)) * manager_player.GetSkillBonus(2);

		// Dexterity Stats
		max_stamina = Mathf.FloorToInt((BASE_STAMINA + (10 * dexterity)) * manager_player.GetSkillBonus(3));
		movement_speed = (((BASE_MOVEMENT_SPEED + (0.05f * dexterity)) * (equipped_armour ? equipped_armour.GetMovementModifier() : 1.0f)) * manager_player.GetSkillBonus(4)) / encumbered_modifier ;
		attack_speed = (BASE_ATTACK_SPEED / (equipped_weapon ? equipped_weapon.GetSpeedModifier() : 1.0f)) / manager_player.GetSkillBonus(5);

		// Intelligence Stats
		experience_gain = (BASE_EXPERIENCE_GAIN + manager_player.GetSkillBonus(6));
		healing_efficacy = (BASE_HEALING_EFFICACY + manager_player.GetSkillBonus(7));
		damage_reduction = (BASE_DAMAGE_REDUCTION + manager_player.GetSkillBonus(8));
	}

	public void Attack() {
		if(canAttack) {
			// TODO: Set isAttacking to false once animation is complete
			isAttacking = true;
			equipped_weapon.UseWeapon(damage);
			canAttack = false;
			attack_timer = attack_speed;
		}
	}
	public new int TakeDamage(int damage) {
		int taken_damage = Mathf.CeilToInt((damage - equipped_armour.GetDefense()) / damage_reduction);
		current_health -= taken_damage;
		if (current_health < 0) {
			current_health = 0;
		}
		return current_health;
	}

	private void FullHeal() {
		current_stamina = max_stamina;
		current_health = max_health;
		manager_player.UpdateUIHealth(current_health, max_health);
		manager_player.UpdateUIStamina(current_stamina, max_stamina);
	}

	public void HealPlayer(int heal) {
		int total_heal = Mathf.FloorToInt(heal * healing_efficacy);
		current_health += heal;
		if(current_health > max_health) {
			current_health = max_health;
		}
		// Update UI
		manager_player.UpdateUIHealth(current_health, max_health);
	}

	public void GainExperience(int experience) {
		current_experience += Mathf.FloorToInt(experience * experience_gain);
		// Check LevelUp
		if(current_experience > current_next_level) {
			LevelUp();
		}
		else {
			// Update UI
			manager_player.UpdateUIExperience(current_level, current_experience, current_next_level);
		}
	}
	private void LevelUp() {
		// Award Stat and Skill Points
		stat_points += 2;
		skill_points += 1;

		// Level Player
		current_level++;
		current_experience -= current_next_level;
		current_next_level = Mathf.FloorToInt(current_next_level * EXPERIENCE_GROWTH);

		// Update UI
		manager_player.UpdateUIExperience(current_level, current_experience, current_next_level);
	}

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
