using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manages player inputs.
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class InputManager : MonoBehaviour {

	[SerializeField] LootManager manager_loot;
	[SerializeField] PlayerManager manager_player;

	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		manager_player.FaceDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		manager_player.SetSprint(Input.GetKey("left shift"));
		if (Input.GetMouseButtonDown(0)) {
			manager_player.TryAttack();
		}		
		if(Input.GetKeyDown("e"))
        {
			manager_loot.LootKeyPressed();
        }
	}

	private void FixedUpdate() {
	}
}
