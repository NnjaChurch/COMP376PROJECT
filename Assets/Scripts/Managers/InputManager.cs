using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manages player inputs.
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class InputManager : MonoBehaviour {

	// Manager References
	[SerializeField] LootManager manager_loot;
	[SerializeField] PlayerManager manager_player;
	[SerializeField] UIManager manager_ui;

	public bool Initialize() {
		Debug.Log("Initializing InputManager...");
		return true;
	}

	// Update is called once per frame
	void Update() {
		manager_player.FaceDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		manager_player.SetSprint(Input.GetKey("left shift"));
		if (Input.GetMouseButton(0)) {
			manager_player.TryAttack();
		}		
		if(Input.GetKeyDown("e"))
        {
			manager_loot.LootKeyPressed();
        }

		if (Input.GetKeyDown(KeyCode.B))
        {
			manager_ui.ToggleInventoryUI();
        }
		if (Input.GetKeyDown(KeyCode.V))
		{
			manager_ui.ToggleStatsUI();
		}
	}

	private void FixedUpdate() {
	}
}
