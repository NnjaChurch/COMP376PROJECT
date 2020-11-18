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
	[SerializeField] PlayerStats player_stats;
	[SerializeField] PlayerSkills player_skills;

	// Manager Class References
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] UIManager manager_UI;

	public float GetSkillBonus(int skill_number) {
		return player_skills.GetSkillBonus(skill_number);
	}

	// TODO: Add Skill Getters when needed

	// TODO: Function to handle attacks inputs from InputManager

	public void consume(Consumable c)
    {
		Debug.Log("TODO consume()");
		// TODO implement the actual healing of the player here ... or delegate to player_stats
    }

	// TODO someone has to call the following update ui functions
	public void updatePlayerHealthInUI()
	{
		manager_UI.updatePlayerHealth();
	}

	public void updatePlayerStaminaInUI()
	{
		manager_UI.updatePlayerStamina();
	}

	public void updatePlayerExperienceInUI()
	{
		manager_UI.updatePlayerExperience();
	}

	public void updatePlayerEquippedWeaponInUI()
	{
		manager_UI.updatePlayerEquippedWeapon();
	}

	public void updatePlayerEquippedArmourInUI()
	{
		manager_UI.updatePlayerEquippedArmour();
	}

	public void updatePlayerSkillsInUI()
	{
		manager_UI.updatePlayerSkills();
	}

	public void updatePlayerInventoryInUI()
	{   // This function is already being called in the right places
		manager_UI.updateInventoryUI();
    }
}