using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manages entity movement
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Movement : MonoBehaviour {
	[SerializeField] private float movement_speed = 1.0f;
	[SerializeField] private float sprint_multiplier = 2.0f;
	private Vector2 movement;
	private bool sprint;
	public Rigidbody2D rb;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void FixedUpdate() {
		movement = Vector2.ClampMagnitude(movement, 1.0f) * movement_speed * Time.deltaTime;
		if (sprint) {
			movement = movement * sprint_multiplier;
		}
		rb.MovePosition(rb.position + movement);
	}

	public void SetMovement(float x, float y) {
		movement.x = x;
		movement.y = y;
	}

	public void SetSprint(bool b) {
		sprint = b;
	}

	public void SetSpeed(float speed) {
		movement_speed = speed;
	}
}
