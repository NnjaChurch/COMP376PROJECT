﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manager that handles Player Stats and Skills
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class StatsManager : MonoBehaviour {

	// Stat Class References
	[SerializeField] PlayerStats player_stats;
	[SerializeField] PlayerSkills player_skills;

	// Manager Class References
	[SerializeField] EquipmentManager manager_equipment;

	public float GetSkillBonus(int skill_number) {
		return player_skills.GetSkillBonus(skill_number);
	}

	// TODO: Add Skill Getters when needed
}