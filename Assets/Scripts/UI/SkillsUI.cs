using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the skills upgrade User Interface
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class SkillsUI : MonoBehaviour {

	[SerializeField] UIManager manager_ui;

	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void UpgradeSkill(int skill_number) {
		manager_ui.UpgradeSkill(skill_number);
	}

	public void UpdateSkillsUI(int skill_points, List<Skill> skills_list) {
		// TODO: Use skill information to upate UI
	}


}
