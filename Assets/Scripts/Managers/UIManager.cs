using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// UI Class References
	[SerializeField] CharacterUI UI_character;
	[SerializeField] InventoryUI UI_inventory;
	[SerializeField] GameUI UI_game;
	[SerializeField] SkillsUI UI_skills;
	[SerializeField] UpgradeUI UI_upgrade;
	[SerializeField] SafeZoneInventoryUI UI_safezone_inventory;

	// Menu Class References
	[SerializeField] LootMenu menu_loot;
	[SerializeField] PauseMenu menu_pause;

	// Manager Class References
	[SerializeField] PlayerManager manager_player;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] SaveManager manager_save;
	[SerializeField] StageManager manager_stage;
	//[SerializeField] LootManager manager_loot;	TODO: Hook up LootManager once implemented

	[SerializeField] GameObject prefab_items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	public void UpdateInventoryUI() {
		InventoryUI.inventory_updated = true; // This lets it know that it should update in the next frame
	}

	public void UpdateSafeZoneInventoryUI()
    {
		UI_safezone_inventory.updateInventoryUI();
    }

	public GameObject GetPrefabItems()
	{
		return prefab_items; // Called in Start() of InventoryUI
	}

	// ----------------------------------------------------- UI_game -----------------------------------------------------------------------//

	public void UpdatePlayerHealth(int current_health, int max_health) {
		UI_game.updatePlayerHealth(current_health, max_health);
		UI_character.updatePlayerHealth(current_health, max_health);
	}

	public void UpdatePlayerStamina(int current_stamina, int max_stamina) {
		UI_game.updatePlayerStamina(current_stamina, max_stamina);
		UI_character.updatePlayerStamina(current_stamina, max_stamina);
	}

	public void UpdatePlayerExperience(int level, int current_experience, int next_level, int banked_exp) {
		// TODO: Add tracker for Banked Experience
		UI_game.updatePlayerExperience(level, current_experience, next_level, banked_exp);
		UI_character.updatePlayerExperience(level, current_experience, next_level, banked_exp);
	}
	public void ToggleInventoryUI()
	{
		UI_inventory.ButtonInventoryClick();
	}

	public void ToggleStatsUI()
	{
		UI_character.ButtonStatsClick();
	}

	// ----------------------------------------------------- UI_character -------------------------------------------------------------------//

	public void UpdatePlayerEquippedWeapon() {
		UI_character.updateEquippedWeapon();
	}

	public void UpdatePlayerEquippedArmour() {
		UI_character.updateEquippedArmour();
	}

	public void UpdatePlayerStats(string[] stat_names, float[] stat_values) {
		UI_character.updatePlayerStats(stat_names, stat_values);
	}

	// ----------------------------------------------------- menu_loot -------------------------------------------------------------------//

	public void DisplayLoot(Lootbag loot)
    {
		menu_loot.DisplayLoot(loot);
    }

	// ----------------------------------------------------- manager_inventory ----------------------------------------------------------------//

	public void Consume(string item) // Called from InventoryUI.cs
    {
		manager_inventory.Consume(item);
    }

	public void AddToInventory(string item)
	{
		manager_inventory.AddToInventory(item);
	}

	public void RemoveFromInventory(string item)
    {
		manager_inventory.RemoveFromInventory(item);
    }

	public IDictionary<string, int> GetConsumables()
	{
		return manager_inventory.GetConsumables();
	}

	public IDictionary<string, int> GetMaterials()
	{
		return manager_inventory.GetMaterials();
	}

	public IDictionary<string, int> GetFoundWeapons()
    {
		return manager_inventory.GetFoundWeapons();
    }

	public IDictionary<string, int> GetFoundArmour()
	{
		return manager_inventory.GetFoundArmour();
	}

	public float GetWeight()
	{
		return manager_inventory.GetWeight();
	}

	// ----------------------------------------------------- manager_equipment ----------------------------------------------------------------//

	public void EquipWeapon(string weapon)
    {
		manager_equipment.EquipWeapon(weapon);
    }

	public void EquipArmour(string armour)
	{
		manager_equipment.EquipArmour(armour);
	}

	public int GetWeaponWeight(string weapon_name)
    {
		return manager_equipment.GetWeaponWeight(weapon_name);
    }

	public int GetWeaponDamage(string weapon_name)
	{
		return manager_equipment.GetWeaponDamage(weapon_name);
	}
	public int GetArmourWeight(string armour_name)
	{
		return manager_equipment.GetArmourWeight(armour_name);
	}

	public int GetArmourDefense(string armour_name)
	{
		return manager_equipment.GetArmourDefense(armour_name);
	}

	public Weapon GetEquippedWeapon()
    {
		return manager_equipment.GetEquippedWeapon();
    }

	public Armour GetEquippedArmour()
	{
		return manager_equipment.GetEquippedArmour();
	}

	// ----------------------------------------------------- manager_player ----------------------------------------------------------------//

	public float GetMaxWeight()
    {
		return manager_player.GetCarryWeight();
    }

	public bool GetZone2Unlocked()
    {
		return manager_player.GetZone2Unlocked();
    }

	public bool GetZone3Unlocked()
	{
		return manager_player.GetZone3Unlocked();
	}

	// ----------------------------------------------------- manager_save -----------------------------------------------------------------------//

	public void SaveGame()
	{
		manager_save.SaveGame();
	}

	// ---------------------------------------------------- manager_stage -------------------------------------------------------------------//

	public void TravelZone(string zone_name)
    {
		manager_stage.TravelZone(zone_name);
    }

	public void UpgradeSkill(int skill_number) {
		manager_player.UpgradeSkill(skill_number);
	}

	public void UpdateSkillsUI(int skill_points, List<Skill> skills_list) {
		UI_skills.UpdateSkillsUI(skill_points, skills_list);
	}

}
