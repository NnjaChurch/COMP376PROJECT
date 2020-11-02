using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the creation and manipulation of skills
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Skill : MonoBehaviour {

	

	const float BASE_VALUE = 1;

	string skill_name;
	int current_level;
	int max_level;
	float per_level;
	float current_bonus;

	public Skill(string name, int max, float per) {
		skill_name = name;
		current_level = 0;
		max_level = max;
		per_level = per;
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

	public string GetName() {
		return skill_name;
	}
	public float GetBonus() {
		return current_bonus;
	}

}