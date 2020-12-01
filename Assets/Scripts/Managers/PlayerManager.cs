using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manager that handles Player Stats and Skills
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class PlayerManager : MonoBehaviour {

	// Stat Class References
	[SerializeField] Movement player_movement;
	[SerializeField] PlayerStats player_stats;
	[SerializeField] PlayerSkills player_skills;


	// Manager Class References
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] UIManager manager_UI;
	[SerializeField] SaveManager manager_save;

	public float GetSkillBonus(int skill_number) {
		return player_skills.GetSkillBonus(skill_number);
	}

	// TODO: Add Skill Information Getters when needed

	// TODO: Function to handle attacks inputs from InputManager
	public void TryAttack() {
		player_stats.Attack();
	}

	public void SetSprint(bool key) {
		player_stats.SetSprint(key);
		if(key && player_stats.GetCurrentStamina() > 0) {
			player_movement.SetSprint(true);
		}
		else {
			player_movement.SetSprint(false);
		}
	}

	public void FaceDirection(float h_axis, float v_axis) {
		player_movement.SetMovement(h_axis, v_axis);
	}

	public void EquipWeapon(Weapon w) {
		player_stats.SetEquippedWeapon(w);
	}

	public void EquipArmour(Armour a) {
		player_stats.SetEquippedArmour(a);
	}

	public void HealPlayer(int value) {
		player_stats.HealPlayer(value);
	}

	public float GetCarryWeight() {
		return player_stats.GetCarryWeight();
	}

	public Vector3 GetPlayerPosition()
    {
		// TODO this possibly returns the PlayerManager's position instead and not the player's position
		return transform.position;
    }


	// UI Functions
	public void UpdateUIHealth(int current_health, int max_health) {
		manager_UI.UpdatePlayerHealth(current_health, max_health);
	}

	public void UpdateUIStamina(int current_stamina, int max_stamina) {
		manager_UI.UpdatePlayerStamina(current_stamina, max_stamina);
	}

	public void UpdateUIExperience(int level, int current_experience, int next_level) {
		manager_UI.UpdatePlayerExperience(level, current_experience, next_level);
	}

	public void UpdateUISkills(int strength, int dexterity, int intelligence) {
		manager_UI.UpdatePlayerSkills(strength, dexterity, intelligence);
	}

	public void UpdateSpeed(float speed) {
		player_movement.SetSpeed(speed);
	}

	// Save Functions
	public bool CheckSave() {
		return manager_save.CheckSave();
	}

	public List<int> SavePlayerStats() {
		List<int> stat_save = new List<int>();

		stat_save.Add(player_stats.GetStrength());
		stat_save.Add(player_stats.GetDexterity());
		stat_save.Add(player_stats.GetIntelligence());
		stat_save.Add(player_stats.GetCurrentLevel());
		stat_save.Add(player_stats.GetCurrentExperience());
		stat_save.Add(player_stats.GetCurrentNextLevel());
		stat_save.Add(player_stats.GetStatPoints());
		stat_save.Add(player_stats.GetSkillPoints());

		return stat_save;
	}

	public List<int> SavePlayerSkills() {
		List<int> skill_save = new List<int>();

		for(int i = 0; i < 9; i++) {
			skill_save.Add(player_skills.GetSkillLevel(i));
		}
		return skill_save;
	}

	public List<int> LoadPlayerStats() {
		return manager_save.LoadPlayerStats();
	}

	public List<int> LoadPlayerSkills() {
		return manager_save.LoadPlayerSkills();
	}
}