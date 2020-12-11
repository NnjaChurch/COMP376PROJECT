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
	[SerializeField] Player player;

	[SerializeField] GameObject player_object;


	// Manager Class References
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] UIManager manager_UI;
	[SerializeField] SaveManager manager_save;
	[SerializeField] StageManager manager_stage;

	[SerializeField] GameObject soundPlayerDeath;

	public bool Initialize() {
		Debug.Log("Initializing PlayerManager...");
		player_skills.Initialize();
		player_stats.Initialize();
		return true;
	}

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

	public int GetStatPoints() {
		return player_stats.GetStatPoints();
	}

	public void UpgradeStat(string stat_name) {
		bool level_success;
		if (player_stats.GetStatPoints() > 0) {
			level_success = player_stats.UpgradeStat(stat_name);
			if (level_success) {
				player_stats.UseStatPoint();
			}
		}
		UpdateUIStats();
	}

	public void UpgradeSkill(int skill_number) {
		bool level_success;
		if (player_stats.GetSkillPoints() > 0) {
			level_success = player_skills.UpgradeSkill(skill_number);
			if (level_success) {
				player_stats.UseSkillPoint();
			}
		}
		UpdateUISkills();
	}


	// UI Functions
	public void UpdateSafeZoneUI() {

	}
	public void UpdateGameUI() {

	}


	public void UpdateUIHealth() {
		manager_UI.UpdatePlayerHealth(player_stats.GetCurrentHealth(), player_stats.GetMaxHealth());
	}

	public void UpdateUIStamina() {
		manager_UI.UpdatePlayerStamina(player_stats.GetCurrentStamina(), player_stats.GetMaxStamina());
	}

	public void UpdateUIExperience() {
		manager_UI.UpdatePlayerExperience(player_stats.GetCurrentLevel(), player_stats.GetCurrentExperience(), player_stats.GetCurrentNextLevel(), player_stats.GetBankedExperience());
	}

	public void UpdateUIStats() {
		manager_UI.UpdatePlayerStats(player_stats.GetUIStats());
	}

	public void UpdateUISkills() {
		manager_UI.UpdateSkillsUI(player_stats.GetSkillPoints(), player_skills.GetSkillsList());
	}

	public void UpdateUIInventory() {
		manager_UI.UpdateInventoryUI();
	}

	public void UpdateSpeed() {
		player_movement.SetSpeed(player_stats.GetMovementSpeed());
	}

	// Save Functions
	public bool CheckSave() {
		return manager_save.CheckSave();
	}

	public void TravelSafeZone(int zone_number) {
		// Function to Unlock Zone
		if (zone_number == 2 && !player_stats.Zone2Unlocked()) {
			player_stats.UnlockZone2();
		}
		if (zone_number == 3 && !player_stats.Zone3Unlocked()) {
			player_stats.UnlockZone3();
		}
		if(zone_number == 4) {
			manager_stage.TravelBossFight();
		}

		manager_stage.TravelSafeZone();
	}

	public bool GetZone2Unlocked() {
		return player_stats.Zone2Unlocked();
	}

	public bool GetZone3Unlocked() {
		return player_stats.Zone3Unlocked();
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
		Instantiate(soundPlayerDeath, player_object.transform);

		manager_inventory.RemoveAllMaterialsAndConsumables();

		player_stats.HalveExperience();
		manager_stage.TravelSafeZone();
	}

	public void SetLookDirection(Vector2 direction)
	{
		direction = (direction - (Vector2)player_object.transform.position).normalized;
		player_object.transform.up = direction;
	}
	public Camera GetPlayerCam()
	{
		return player.GetPlayerCam();
	}
}