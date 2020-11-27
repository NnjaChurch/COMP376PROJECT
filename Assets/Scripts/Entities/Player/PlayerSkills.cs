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
	[SerializeField] PlayerManager manager_player;

	void Start() {
		InitializeSkills();

		if(manager_player.CheckSave()) {
			List<int> skills_load = manager_player.LoadPlayerSkills();
			for(int i = 0; i < player_skills.Count; i++) {
				player_skills[i].SetLevel(skills_load[i]);
			}
		}
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

	public void SetSkillLevel(int skill_number, int level) {
		player_skills[skill_number].SetLevel(level);
	}

	public string GetSkillName(int skill_number) {
		return player_skills[skill_number].GetName();
	}

	public string GetSkillDescription(int skill_number) {
		return player_skills[skill_number].GetDescription();
	}

	public int GetSkillLevel(int skill_number) {
		return player_skills[skill_number].GetLevel();
	}

	public float GetSkillBonus(int skill_number) {
		return player_skills[skill_number].GetBonus();
	}
}
