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

	public float GetSkillBonus(int skill_number) {
		return player_skills.GetSkillBonus(skill_number);
	}

	// TODO: Add Skill Getters when needed

	// TODO: Function to handle attacks inputs from InputManager
}