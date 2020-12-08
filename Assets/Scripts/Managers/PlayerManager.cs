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

	[SerializeField] GameObject player_object;


	// Manager Class References
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] UIManager manager_UI;
	[SerializeField] SaveManager manager_save;
	[SerializeField] StageManager manager_stage;

	[SerializeField] AudioSource audioPlayerDeath;

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
		if (key && player_stats.GetCurrentStamina() > 0) {
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

	public void SetEncumberance(float value) {
		player_stats.SetEncumberance(value);
	}

	public float GetCarryWeight() {
		return player_stats.GetCarryWeight();
	}

	public Vector3 GetPlayerPosition() {
		return player_object.transform.position;
	}

	public int GetPlayerLevel() {
		return player_stats.GetPlayerLevel();
	}

	public void UpgradeSkill(int skill_number) {
		player_skills.UpgradeSkill(skill_number);
		UpdateUISkills();
	}


	// UI Functions
	public void UpdateUIHealth(int current_health, int max_health) {
		manager_UI.UpdatePlayerHealth(current_health, max_health);
	}

	public void UpdateUIStamina(int current_stamina, int max_stamina) {
		manager_UI.UpdatePlayerStamina(current_stamina, max_stamina);
	}

	public void UpdateUIExperience(int level, int current_experience, int next_level, int banked_exp) {
		manager_UI.UpdatePlayerExperience(level, current_experience, next_level, banked_exp);
	}

	public void UpdateUIStats(string[] stat_names, float[] stat_values) {
		manager_UI.UpdatePlayerStats(stat_names, stat_values);
	}

	public void UpdateUISkills() {
		// TODO: Pass Skill Points, Skill Information.
		int skill_points = player_stats.GetSkillPoints();
		List<Skill> skills_list = player_skills.GetSkillsList();
		manager_UI.UpdateSkillsUI(skill_points, skills_list);
	}

	public void UpdateSpeed(float speed) {
		player_movement.SetSpeed(speed);
	}

	// Save Functions
	public bool CheckSave() {
		return manager_save.CheckSave();
	}

	public void TravelSafeZone(int zone_number) {
		// Function to Unlock Zone
		if(zone_number == 2 && !player_stats.Zone2Unlocked()) {
			player_stats.UnlockZone2();
		}
		if(zone_number == 3 && !player_stats.Zone3Unlocked()) {
			player_stats.UnlockZone3();
		}

		manager_stage.TravelSafeZone();
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
		stat_save.Add(player_stats.GetBankedExperience());
		stat_save.Add(System.Convert.ToInt32(player_stats.Zone2Unlocked()));
		stat_save.Add(System.Convert.ToInt32(player_stats.Zone3Unlocked()));

		return stat_save;
	}

	public List<int> SavePlayerSkills() {
		List<int> skill_save = new List<int>();

		for (int i = 0; i < 9; i++) {
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

	public void KillPlayer() {
		audioPlayerDeath.Play();

		// TODO: Function to Purge Consumables and Materials from Inventory

		player_stats.HalveExperience();
		manager_stage.TravelSafeZone();
	}
}