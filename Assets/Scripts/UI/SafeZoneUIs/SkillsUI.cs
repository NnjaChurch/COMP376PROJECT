using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the skills upgrade User Interface
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class SkillsUI : MonoBehaviour {

	[SerializeField] UIManager manager_ui;

	[SerializeField] Text[] skills_texts;
	[SerializeField] Text remainingPointsText;

	public void Initialize() {
		Debug.Log("Initializing SkillsUI...");
	}

	// Update is called once per frame
	void Update() {

	}

	public void UpgradeSkill(int skill_number) {
		manager_ui.UpgradeSkill(skill_number);
	}

	public void UpdateSkillsUI(int skill_points, List<Skill> skills_list) {
		for (int i = 0; i < skills_texts.Length; i++)
        {
			skills_texts[i].text = skills_list[i].GetName() + ": " + skills_list[i].GetLevel();
        }

		remainingPointsText.text = "Remaining Skill Points: " + skill_points;
	}


}
