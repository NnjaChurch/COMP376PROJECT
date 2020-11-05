using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manages player inputs.
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class InputManager : MonoBehaviour {
	public Player player;
	public Movement player_movement;


	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		player_movement.SetMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		player_movement.SetSprint(Input.GetKey("left shift"));
		if (Input.GetMouseButtonDown(0)) {
			player.Attack(player.GetStats().GetDamage());
		}		
		
	}

	private void FixedUpdate() {
	}
}
