using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the creation and manipulation of skills
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Skill {

	const float BASE_VALUE = 1;

	string skill_name;
	string skill_stat;
	string skill_description;
	int current_level;
	int max_level;
	float per_level;
	float current_bonus;

	public Skill(string name, string stat, int max, float per) {
		skill_name = name;
		skill_stat = stat;
		current_level = 0;
		max_level = max;
		per_level = per;
		current_bonus = BASE_VALUE;
		GenerateDescription();
	}
	public bool LevelSkill() {
		if(current_level < max_level) {
			current_level++;
			current_bonus += per_level;
			return true;
		}
		else {
			return false;
		}
	}

	private void GenerateDescription() {
		skill_description = "Level: " + current_level + " / " + max_level + "\n"
			+ "Increases " + skill_stat + " by " + per_level + " per level.\n"
			+ "Current Bonus: " + current_bonus + "%\n"
			+ "Next Level: " + (current_bonus + per_level) + "%";
	}

	public string GetName() {
		return skill_name;
	}
	
	public string GetDescription() {
		return skill_description;
	}

	public float GetBonus() {
		return current_bonus;
	}
}