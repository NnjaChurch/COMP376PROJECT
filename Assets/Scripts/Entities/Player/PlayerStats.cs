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
	[SerializeField] int BASE_STAMINA_REGEN = 5;
	[SerializeField] float BASE_STAMINA_REGEN_CD = 3; // seconds
	[SerializeField] int BASE_CARRY_WEIGHT = 50;        // lbs
	[SerializeField] int BASE_EXPERIENCE_GAIN = 0;      // %
	[SerializeField] int BASE_HEALING_EFFICACY = 0;     // %
	[SerializeField] int BASE_DAMAGE_REDUCTION = 0;     // %
	[SerializeField] int BASE_EXPERIENCE_LEVEL = 100;   // %
	[SerializeField] float EXPERIENCE_GROWTH = 1.2f;
	[SerializeField] AudioSource audioTakeDamage;
	[SerializeField] AudioSource[] audioAttacks;

	// Calculated Stats
	int max_stamina;
	float carry_weight;
	float experience_gain;
	float healing_efficacy;
	float damage_reduction;
	float encumbered_modifier;

	// Current Stats
	int current_stamina;
	int banked_experience;

	// Stamina System
	float second_timer;
	int stamina_regen;
	float current_stamina_regen_cd;
	bool isSprinting;

	// Zone System
	bool zone2_unlocked;
	bool zone3_unlocked;


	// Manager Reference
	[SerializeField] PlayerManager manager_player;

	public void Initialize() {
		Debug.Log("Initializing PlayerStats...");

		// Check for Save File
		if (manager_player.CheckSave()) {
			List<int> stats_load = manager_player.LoadPlayerStats();

			strength = stats_load[0];
			dexterity = stats_load[1];
			intelligence = stats_load[2];

			current_level = stats_load[3];
			current_experience = stats_load[4];
			current_next_level = stats_load[5];

			stat_points = stats_load[6];
			skill_points = stats_load[7];

			banked_experience = stats_load[8];

			if (stats_load[9] == 1) {
				zone2_unlocked = true;
			}
			else {
				zone2_unlocked = false;
			}
			if (stats_load[10] == 1) {
				zone3_unlocked = true;
			}
			else {
				zone3_unlocked = false;
			}
		}
		else {
			zone2_unlocked = false;
			zone3_unlocked = false;
			InitalizeExperience();
		}

		// Variable Initialization
		canAttack = true;
		isAttacking = false;
		isSprinting = false;
		encumbered_modifier = 1.0f;
		attack_timer = attack_speed;

		// Calculate Stats and Heal Player
		CalculatePlayerStats();
		FullHeal();
		// Gain Banked Experience (Banked Experience will be 0 if player is entering a Zone)
		GainExperience();
		UpdateAllUI();
	}

	private void Update() {
		// Attack Cooldown
		if (attack_timer > 0) {
			attack_timer -= Time.deltaTime;
			if (attack_timer < 0) {
				attack_timer = 0;
			}
			if (attack_timer == 0) {
				canAttack = true;
			}
		}
		// Stamina System
		// TODO: Possibly Simplify if statement cases to make code more efficient
		if (isSprinting && current_stamina > 0) {
			current_stamina_regen_cd = BASE_STAMINA_REGEN_CD;
			second_timer += Time.deltaTime;
			if (second_timer > 1) {
				current_stamina -= 10;
				if (current_stamina < 0) {
					current_stamina = 0;
				}
				second_timer -= 1;
				//Debug.Log("Player Stamina reduced by 1 (Current: " + current_stamina + ")");
				manager_player.UpdateUIStamina();
			}
		}
		else {
			if (current_stamina_regen_cd == 0) {
				second_timer += Time.deltaTime;
				if (second_timer > 1 && current_stamina < max_stamina) {
					current_stamina += stamina_regen;
					if (current_stamina > max_stamina) {
						current_stamina = max_stamina;
					}
					//Debug.Log("Player Stamina restored by " + stamina_regen + " (Current: " + current_stamina + ")");
					second_timer -= 1;
					manager_player.UpdateUIStamina();
				}
			}
			if (current_stamina_regen_cd > 0) {
				current_stamina_regen_cd -= Time.deltaTime;
				if (current_stamina_regen_cd < 0) {
					current_stamina_regen_cd = 0;
				}
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
		movement_speed = (BASE_MOVEMENT_SPEED + (0.05f * (float)dexterity)) * CalculateArmourBonus() * manager_player.GetSkillBonus(4) / encumbered_modifier;
		attack_speed = (BASE_ATTACK_SPEED / CalculateWeaponBonus()) / manager_player.GetSkillBonus(5);
		stamina_regen = BASE_STAMINA_REGEN + Mathf.FloorToInt(max_stamina / 50) - 1; // Should increase by 1 for every 50 stamina after the base 100

		// Intelligence Stats
		experience_gain = (BASE_EXPERIENCE_GAIN + ((float)intelligence * 0.01f) + manager_player.GetSkillBonus(6));
		healing_efficacy = (BASE_HEALING_EFFICACY + ((float)intelligence * 0.01f) + manager_player.GetSkillBonus(7));
		damage_reduction = (BASE_DAMAGE_REDUCTION + ((float)intelligence * 0.01f) + manager_player.GetSkillBonus(8));

		UpdateAllUI();
	}

	private void InitalizeExperience() {
		current_level = 1;
		current_experience = 0;
		banked_experience = 0;
		current_next_level = BASE_EXPERIENCE_LEVEL;
	}

	public void Attack() {
		if (canAttack) {
			// TODO: Set isAttacking to false once animation is complete
			isAttacking = true;
			audioAttacks[Random.Range(0, audioAttacks.Length)].Play();
			equipped_weapon.UseWeapon(damage, this);
			canAttack = false;
			attack_timer = attack_speed;
		}
	}

	public void SetSprint(bool sprint) {
		// Reset Second timer if sprint state changes
		if (isSprinting != sprint) {
			second_timer = 0;
		}
		isSprinting = sprint;
	}
	public int TakeDamage(int damage) {
		audioTakeDamage.Play();
		int taken_damage = Mathf.CeilToInt((damage - equipped_armour.GetDefense()) / damage_reduction);
		current_health -= taken_damage;
		if (current_health < 0) {
			current_health = 0;
		}
		if (current_health == 0) {
			manager_player.KillPlayer();
		}

		manager_player.UpdateUIHealth();

		return current_health;
	}

	private void FullHeal() {
		current_stamina = max_stamina;
		current_health = max_health;
		manager_player.UpdateUIHealth();
		manager_player.UpdateUIStamina();
	}

	public void HealPlayer(int heal) {
		int total_heal = Mathf.FloorToInt(heal * healing_efficacy);
		current_health += heal;
		if (current_health > max_health) {
			current_health = max_health;
		}
		// Update UI
		manager_player.UpdateUIHealth();
	}

	public void CollectExperience(int experience) {
		print("Gained " + experience + " experience!");
		banked_experience += experience;
		manager_player.UpdateUIExperience();
	}

	public void HalveExperience() {
		banked_experience = Mathf.FloorToInt((float)banked_experience / 2);
	}

	public void GainExperience() {
		current_experience += Mathf.FloorToInt((float)banked_experience * experience_gain); 
		banked_experience = 0;
		// Check LevelUp
		if (current_experience > current_next_level) {
			LevelUp();
		}
		else {
			// Update UI
			manager_player.UpdateUIExperience();
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
		manager_player.UpdateUIExperience();
	}

	private float CalculateWeaponBonus() {
		if (equipped_weapon != null) {
			return equipped_weapon.GetSpeedModifier();
		}
		else {
			return 1.0f;
		}
	}
	private float CalculateArmourBonus() {
		if (equipped_armour != null) {
			return equipped_armour.GetMovementModifier();
		}
		else {
			return 1.0f;
		}
	}

	public void SetEncumberance(float value) {
		encumbered_modifier = value;
	}

	public int GetCurrentStamina() { return current_stamina; }
	public int GetMaxStamina() { return max_stamina; }
	public float GetCarryWeight() { return carry_weight; }
	public int GetCurrentExperience() { return current_experience; }
	public int GetCurrentNextLevel() { return current_next_level; }
	public int GetStrength() { return strength; }
	public int GetDexterity() { return dexterity; }
	public int GetIntelligence() { return intelligence; }
	public int GetStatPoints() { return stat_points; }
	public int GetSkillPoints() { return skill_points; }
	public int GetBankedExperience() { return banked_experience; }
	public bool Zone2Unlocked() { return zone2_unlocked; }
	public bool Zone3Unlocked() { return zone3_unlocked; }
	public int GetPlayerLevel() { return current_level; }

	public bool UpgradeStat(string stat_name) {
		bool stat_upgraded;
		switch (stat_name) {
			case "Strength":
				strength++;
				stat_upgraded = true;
				break;
			case "Dexterity":
				dexterity++;
				stat_upgraded = true;
				break;
			case "Intelligence":
				intelligence++;
				stat_upgraded = true;
				break;
			default:
				stat_upgraded = false;
				break;
		}
		if(stat_upgraded) {
			CalculatePlayerStats();
			FullHeal();
			UpdateAllUI();
		}
		return stat_upgraded;
	}

	public void UseSkillPoint() {
		skill_points--;
		CalculatePlayerStats();
		FullHeal();
		UpdateAllUI();
	}
	public void UseStatPoint() {
		stat_points--;
	}

	public void UnlockZone2() {
		zone2_unlocked = true;
	}
	public void UnlockZone3() {
		zone3_unlocked = true;
	}

	public void SetEquippedWeapon(Weapon w) {
		equipped_weapon = w;
		CalculatePlayerStats();
	}

	public void SetEquippedArmour(Armour a) {
		equipped_armour = a;
		CalculatePlayerStats();
	}

 	public List<float> GetUIStats() {
		return new List<float>() { strength, dexterity, intelligence, damage, movement_speed, attack_speed, stamina_regen, experience_gain, healing_efficacy, damage_reduction };
	}

	private void UpdateAllUI() {
		manager_player.UpdateUIHealth();
		manager_player.UpdateUIStamina();
		manager_player.UpdateSpeed();
		manager_player.UpdateUIExperience();
		manager_player.UpdateUIStats();
		manager_player.UpdateUIInventory();
	}
}
