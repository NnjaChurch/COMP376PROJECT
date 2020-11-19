using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining zombie speicific behaviour and statistics
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Enemy : Entity {

	public EnemyStats stats;
	[SerializeField]
	GameObject lootBagPrefab;
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public Stats GetStats() { return stats; }

	public void Kill() {
		gameObject.SetActive(false);
		GameObject lootbag;
		lootbag = Instantiate(lootBagPrefab, transform.position, transform.rotation) as GameObject;
	}
}
