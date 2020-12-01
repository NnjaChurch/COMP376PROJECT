using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining player specific behaviour and statistics
//	Contributors: Jordan, Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Player : Entity {

	// Component References
	[SerializeField] Camera player_cam;

	// Manager Class References
	[SerializeField] PlayerManager manager_player;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		Vector2 direction = player_cam.ScreenToWorldPoint(Input.mousePosition);
		direction = (direction - (Vector2)gameObject.transform.position).normalized;
		gameObject.transform.up = direction;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "EnemyPerceptionAura")
        {
			collision.gameObject.GetComponentInParent<AIDestinationSetter>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "EnemyPerceptionAura")
		{
			collision.gameObject.GetComponentInParent<AIDestinationSetter>().enabled = false;
		}
	}
}
