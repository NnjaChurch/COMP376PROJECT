﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// UI Class References

	// Non Safe Zone UI Elements
	[SerializeField] CharacterUI UI_character;
	[SerializeField] InventoryUI UI_inventory;
	[SerializeField] GameUI UI_game;

	// Safe Zone UI Elements
	[SerializeField] SkillsUI UI_skills;
	[SerializeField] UpgradeUI UI_upgrade;
	[SerializeField] SafeZoneInventoryUI UI_safezone_inventory;
	[SerializeField] SafeZoneCharacterUI UI_safezone_character;
	[SerializeField] TravelMenuUI UI_Travel;

	// Menu Class References
	[SerializeField] LootMenu menu_loot;
	[SerializeField] PauseMenu menu_pause;

	// Manager Class References
	[SerializeField] PlayerManager manager_player;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] SaveManager manager_save;
	[SerializeField] StageManager manager_stage;

	[SerializeField] GameObject prefab_items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	public bool Initialize() {
		Debug.Log("Initializing UIManager...");
		// Initialize Components based on Safe Zone State
		if (manager_stage.GetSafeZone()) {
			// Initialize Safe Zone UI Components
			UI_skills.Initialize();
			UI_upgrade.Initialize();
			UI_safezone_inventory.Initialize();
			UI_safezone_character.Initialize();
			UI_Travel.Initialize();
			menu_pause.Initialize();

			manager_player.UpdateUIStats();
			manager_player.UpdateUISkills();
			manager_player.UpdateUIExperience();

			UpdatePlayerEquippedWeapon();
			UpdatePlayerEquippedArmour();
		}
		else {
			// Initialize Non Safe Zone UI Components
			UI_character.Initialize();
			UI_inventory.Initialize();
			UI_game.Initialize();
			menu_loot.Initialize();
			menu_pause.Initialize();

			manager_player.UpdateUIStats();

			UpdatePlayerEquippedWeapon();
			UpdatePlayerEquippedArmour();
		}
		return true;
	}

	public void UpdateInventoryUI() {
		InventoryUI.inventory_updated = true; // This lets it know that it should update in the next frame

		if (UI_safezone_inventory != null) {
			UI_safezone_inventory.updateInventoryUI();
		}
	}

	public void UpdateSafeZoneInventoryUI() {
		UI_safezone_inventory.updateInventoryUI();
	}

	public GameObject GetPrefabItems() {
		return prefab_items; // Called in Start() of InventoryUI
	}

	// ----------------------------------------------------- UI_game -----------------------------------------------------------------------//

	public void UpdatePlayerHealth(int current_health, int max_health) {
		if (UI_game != null) {
			UI_game.updatePlayerHealth(current_health, max_health);
		}
		if (UI_character != null) {
			UI_character.updatePlayerHealth(current_health, max_health);
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updatePlayerHealth(current_health, max_health);
		}
	}

	public void UpdatePlayerStamina(int current_stamina, int max_stamina) {
		if (UI_game != null) {
			UI_game.updatePlayerStamina(current_stamina, max_stamina);
		}
		if (UI_character != null) {
			UI_character.updatePlayerStamina(current_stamina, max_stamina);
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updatePlayerStamina(current_stamina, max_stamina);
		}
	}

	public void UpdatePlayerExperience(int level, int current_experience, int next_level, int banked_exp) {
		if (UI_game != null) {
			UI_game.updatePlayerExperience(level, current_experience, next_level, banked_exp);
		}
		if (UI_character != null) {
			UI_character.updatePlayerExperience(level, current_experience, next_level, banked_exp);
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updatePlayerExperience(level, current_experience, next_level, banked_exp);
		}
	}
	public void ToggleInventoryUI() {
		UI_inventory.ButtonInventoryClick();
	}

	public void ToggleStatsUI() {
		UI_character.ButtonStatsClick();
	}

	public void EnableButtons() {
		UI_inventory.EnableButtons();
		menu_loot.EnableButtons();
	}

	public void DisableButtons() {
		UI_inventory.DisableButtons();
		menu_loot.DisableButtons();
	}

	// ----------------------------------------------------- UI_character -------------------------------------------------------------------//

	public void UpdatePlayerEquippedWeapon() {
		if (UI_character != null) {
			UI_character.updateEquippedWeapon();
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updateEquippedWeapon();
		}
	}

	public void UpdatePlayerEquippedArmour() {
		if (UI_character != null) {
			UI_character.updateEquippedArmour();
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updateEquippedArmour();
		}
	}

	public void UpdatePlayerStats(List<float> stat_values) {
		if (UI_character != null) {
			UI_character.updatePlayerStats(stat_values);
		}

		if (UI_safezone_character != null) {
			UI_safezone_character.updatePlayerStats(stat_values);
		}
	}

	// ----------------------------------------------------- menu_loot -------------------------------------------------------------------//

	public void DisplayLoot(Lootbag loot) {
		menu_loot.DisplayLoot(loot);
	}

	// ----------------------------------------------------- manager_inventory ----------------------------------------------------------------//

	public void Consume(string item) {
		manager_inventory.Consume(item);
	}

	public void AddToInventory(string item) {
		manager_inventory.AddToInventory(item);
	}

	public void RemoveFromInventory(string item) {
		manager_inventory.RemoveFromInventory(item);
	}

	public IDictionary<string, int> GetConsumables() {
		return manager_inventory.GetConsumables();
	}

	public IDictionary<string, int> GetMaterials() {
		return manager_inventory.GetMaterials();
	}

	public IDictionary<string, int> GetFoundWeapons() {
		return manager_inventory.GetFoundWeapons();
	}

	public IDictionary<string, int> GetFoundArmour() {
		return manager_inventory.GetFoundArmour();
	}

	public float GetWeight() {
		return manager_inventory.GetWeight();
	}

	// ----------------------------------------------------- manager_equipment ----------------------------------------------------------------//

	public void EquipWeapon(string weapon) {
		manager_equipment.EquipWeapon(weapon);
	}
	public void EquipArmour(string armour) {
		manager_equipment.EquipArmour(armour);
	}
	public int GetWeaponWeight(string weapon_name) {
		return manager_equipment.GetWeaponWeight(weapon_name);
	}
	public int GetWeaponDamage(string weapon_name) {
		return manager_equipment.GetWeaponDamage(weapon_name);
	}
	public int GetArmourWeight(string armour_name) {
		return manager_equipment.GetArmourWeight(armour_name);
	}
	public int GetArmourDefense(string armour_name) {
		return manager_equipment.GetArmourDefense(armour_name);
	}
	public Weapon GetEquippedWeapon() {
		return manager_equipment.GetEquippedWeapon();
	}
	public Armour GetEquippedArmour() {
		return manager_equipment.GetEquippedArmour();
	}

	public List<int> GetWeaponMaterials(string weapon_name) {
		return manager_equipment.GetWeaponMaterials(weapon_name);
	}
	public List<int> GetArmourMaterials(string armour_name) {
		return manager_equipment.GetArmourMaterials(armour_name);
	}
	public List<int> GetWeaponInfo(string weapon_name) {
		return manager_equipment.GetWeaponInfo(weapon_name);
	}
	public List<int> GetArmourInfo(string armour_name) {
		return manager_equipment.GetArmourInfo(armour_name);
	}

	// ----------------------------------------------------- manager_player ----------------------------------------------------------------//

	public float GetMaxWeight() {
		return manager_player.GetCarryWeight();
	}

	public bool GetZone2Unlocked() {
		return manager_player.GetZone2Unlocked();
	}

	public bool GetZone3Unlocked() {
		return manager_player.GetZone3Unlocked();
	}

	public void UpgradeSkill(int skill_number) {
		manager_player.UpgradeSkill(skill_number);
	}

	public int GetRemainingStatPoints() {
		return manager_player.GetStatPoints();
	}

	public void UpgradeStat(string stat_name) {
		manager_player.UpgradeStat(stat_name);
	}

	public void UpgradeWeapon(string weapon_name) {
		bool can_upgrade = true;
		bool upgrade_success = false;
		int iterator = 0;
		List<int> material_cost = manager_equipment.GetWeaponMaterials(weapon_name);
		IDictionary<string, int> materials = manager_inventory.GetMaterials();
		IDictionary<string, int> consumed = new Dictionary<string, int>();

		foreach (KeyValuePair<string, int> material in materials) {

			if (material.Value < material_cost[iterator]) {
				can_upgrade = false;
				break;
			}
			else {
				consumed.Add(material.Key, material_cost[iterator]);
			}
			iterator++;
		}
		if (can_upgrade) {
			upgrade_success = manager_equipment.UpgradeWeapon(weapon_name);
		}
		else {
		}
		if (upgrade_success) {
			foreach (KeyValuePair<string, int> cost in consumed) {
				manager_inventory.UseMaterials(cost.Key, cost.Value);
			}
		}
	}

	public void UpgradeArmour(string armour_name) {
		bool can_upgrade = true;
		bool upgrade_success = false;
		int iterator = 0;
		List<int> material_cost = manager_equipment.GetArmourMaterials(armour_name);
		IDictionary<string, int> materials = manager_inventory.GetMaterials();
		IDictionary<string, int> consumed = new Dictionary<string, int>();

		foreach (KeyValuePair<string, int> material in materials) {
			if (material.Value < material_cost[iterator]) {
				can_upgrade = false;
				break;
			}
			else {
				consumed.Add(material.Key, material_cost[iterator]);
			}
			iterator++;
		}
		if (can_upgrade) {
			upgrade_success = manager_equipment.UpgradeArmour(armour_name);
		}
		else {
		}
		if (upgrade_success) {
			foreach (KeyValuePair<string, int> cost in consumed) {
				manager_inventory.UseMaterials(cost.Key, cost.Value);
			}
		}
	}

	// ----------------------------------------------------- manager_save -----------------------------------------------------------------------//

	public void SaveGame() {
		manager_save.SaveGame();
	}

	// ---------------------------------------------------- manager_stage -------------------------------------------------------------------//

	public void TravelZone(string zone_name) {
		manager_stage.TravelZone(zone_name);
	}

	// ----------------------------------------------------- UI_skills -----------------------------------------------------------------------//

	public void UpdateSkillsUI(int skill_points, List<Skill> skills_list) {
		UI_skills.UpdateSkillsUI(skill_points, skills_list);
	}

	// ----------------------------------------------------- UI_upgrade -----------------------------------------------------------------------//

	public void UpdateUpgradeUI() {
		UI_upgrade.UpdateUpgradeUI();
	}

	// ----------------------------------------------------- menu_pause -----------------------------------------------------------------------//

	public bool GetGamePause() {
		return menu_pause.GetGamePaused();
	}

	public void PauseGame() {
		menu_pause.PauseGame();
	}

	public void ResumeGame() {
		menu_pause.ResumeGame();
	}
}
