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
		if (Input.GetMouseButton(0) && !(manager_ui.GetGamePause())) {
			manager_player.TryAttack();
		}		
		if(Input.GetKeyDown("e") && !(manager_ui.GetGamePause()))
        {
			manager_loot.LootKeyPressed();
        }

		if (Input.GetKeyDown(KeyCode.B) && !(manager_ui.GetGamePause()))
        {
			ToggleInventoryUI();
        }
		if (Input.GetKeyDown(KeyCode.V) && !(manager_ui.GetGamePause()))
		{
			ToggleStatsUI();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!(manager_ui.GetGamePause()))
            {
				PauseGame();
            }
			else
            {
				ResumeGame();
            }
        }
	}

	private void FixedUpdate() {
	}

	public void PauseGame()
    {
		manager_ui.PauseGame();
    }

	public void ResumeGame()
    {
		manager_ui.ResumeGame();
    }

	public void ToggleStatsUI()
	{
		manager_ui.ToggleStatsUI();
	}

	public void ToggleInventoryUI()
	{
		manager_ui.ToggleInventoryUI();
	}
}
