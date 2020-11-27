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
		current_level = 0;
		skill_name = name;
		skill_stat = stat;
		max_level = max;
		per_level = per;
		CalculateBonus();
		GenerateDescription();
	}
	public bool LevelSkill() {
		if(current_level < max_level) {
			current_level++;
			CalculateBonus();
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

	public void SetLevel(int level) {
		current_level = level;
		CalculateBonus();
	}

	public string GetName() {
		return skill_name;
	}
	
	public string GetDescription() {
		return skill_description;
	}

	public int GetLevel() {
		return current_level;
	}
	public float GetBonus() {
		return current_bonus;
	}

	private void CalculateBonus() {
		current_bonus = BASE_VALUE + (current_level * per_level);
	}
}