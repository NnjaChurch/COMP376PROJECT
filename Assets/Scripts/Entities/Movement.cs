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

	[SerializeField] Animator player_animator;

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

		if (x == 0 && y == 0)
        {
			player_animator.SetBool("isWalking", false);
        }
		else
        {
			player_animator.SetBool("isWalking", true);
		}
	}

	public void SetSprint(bool b) {
		sprint = b;
		player_animator.SetBool("isRunning", b);
	}

	public void SetSpeed(float speed) {
		movement_speed = speed;
	}
}
