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
	[SerializeField] PlayerManager manager_stats;
	[SerializeField] InventoryManager manager_inventory;
	//[SerializeField] LootManager manager_loot;	TODO: Hook up LootManager once implemented

	public void updatePlayerHealth()
    {
		UI_game.updatePlayerHealth();
    }

	public void updatePlayerStamina()
	{
		UI_game.updatePlayerStamina();
	}

	public void updatePlayerExperience()
	{
		UI_game.updatePlayerExperience();
	}

	public void updatePlayerEquippedWeapon()
	{
		UI_character.updateEquippedWeapon();
	}

	public void updatePlayerEquippedArmour()
	{
		UI_character.updateEquippedArmour();
	}

	public void updatePlayerSkills()
	{
		UI_character.updatePlayerSkills();
	}

}
