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
		GameObject gameObject = collision.gameObject;
		if (gameObject.tag == "EnemyPerceptionAura")
        {
			gameObject.GetComponentInParent<AIPath>().enabled = true;
			gameObject.GetComponentInParent<AIDestinationSetter>().enabled = true;
		}
		if (gameObject.tag == "Roof")
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
		GameObject gameObject = collision.gameObject;
		if (gameObject.tag == "EnemyPerceptionAura")
		{
			gameObject.GetComponentInParent<AIPath>().enabled = false;
			gameObject.GetComponentInParent<AIDestinationSetter>().enabled = false;
		}
		if (gameObject.tag == "Roof")
        {
			gameObject.GetComponent<SpriteRenderer>().enabled = true;
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.gameObject.tag == "Weapon")
        {
			EnemyStats enemyStats = collision.gameObject.transform.GetComponentInParent<EnemyStats>();
			if (enemyStats != null)
			{
				enemyStats.StartAttack();
            }
		}
    }
}
