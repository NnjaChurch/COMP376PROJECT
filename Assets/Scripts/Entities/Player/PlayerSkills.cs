using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the storage of all skills
//	Contributors: Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class PlayerSkills : MonoBehaviour {

	// Variables and Containers
	List<Skill> player_skills;

	// Manager Reference
	[SerializeField] PlayerManager manager_stats;

	void Start() {
		InitializeSkills();
	}

	private void InitializeSkills() {
		// Initialize Skill List
		player_skills = new List<Skill>();
		player_skills.Add(new Skill("Health Boost", "Health", 20, 0.05f));
		player_skills.Add(new Skill("Damage Boost", "Damage", 20, 0.10f));
		player_skills.Add(new Skill("Weight Boost", "Carry Weight", 10, 0.05f));
		player_skills.Add(new Skill("Stamina Boost", "Stamina", 20, 0.05f));
		player_skills.Add(new Skill("Movement Speed Boost", "Movement Speed", 10, 0.10f));
		player_skills.Add(new Skill("Attack Speed Boost", "Attack Speed", 20, 0.05f));
		player_skills.Add(new Skill("Experience Boost", "Experience Gain", 20, 0.05f));
		player_skills.Add(new Skill("Healing Boost", "Healing Efficiency", 10, 0.05f));
		player_skills.Add(new Skill("Damage Reduction Boost", "Damage Reduction", 25, 0.02f));
	}

	public void UpgradeSkill(int skill_number) {
		player_skills[skill_number].LevelSkill();
	}

	public string GetSkillName(int skill_number) {
		return player_skills[skill_number].GetName();
	}

	public string GetSkillDescription(int skill_number) {
		return player_skills[skill_number].GetDescription();
	}

	public float GetSkillBonus(int skill_number) {
		return player_skills[skill_number].GetBonus();
	}
}
