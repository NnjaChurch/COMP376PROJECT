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

	// Menu Class References
	[SerializeField] LootMenu menu_loot;
	[SerializeField] PauseMenu menu_pause;

	// Manager Class References
	[SerializeField] PlayerManager manager_player;
	[SerializeField] InventoryManager manager_inventory;
	//[SerializeField] LootManager manager_loot;	TODO: Hook up LootManager once implemented

	[SerializeField] GameObject prefab_items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	public void UpdateInventoryUI() {
		InventoryUI.inventory_updated = true; // This lets it know that it should update in the next frame
	}

	public void UpdatePlayerHealth(int current_health, int max_health) {
		UI_game.updatePlayerHealth(current_health, max_health);
	}

	public void UpdatePlayerStamina(int current_stamina, int max_stamina) {
		UI_game.updatePlayerStamina(current_stamina, max_stamina);
	}

	public void UpdatePlayerExperience(int level, int current_experience, int next_level) {
		UI_game.updatePlayerExperience(level, current_experience, next_level);
	}

	public void UpdatePlayerEquippedWeapon() {
		UI_character.updateEquippedWeapon();
	}

	public void UpdatePlayerEquippedArmour() {
		UI_character.updateEquippedArmour();
	}

	public void UpdatePlayerSkills(int strength, int dexterity, int intelligence) {
		UI_character.updatePlayerSkills(strength, dexterity, intelligence);
	}

	public void ToggleInventoryUI()
    {
		UI_game.ButtonBClick();
    }

	public void ToggleStatsUI()
    {
		UI_game.ButtonVClick();
    }

	public GameObject GetPrefabItems()
    {
		return prefab_items; // Called in Start() of InventoryUI
    }

}
