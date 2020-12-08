using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining zombie speicific behaviour and statistics
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Enemy : Entity {

	public EnemyStats stats;

	[SerializeField] GameObject lootBagPrefab;
	[SerializeField] GameObject soundDeathPrefab;
	[SerializeField] float talkCooldDownMin;
	[SerializeField] float talkCoolDownRandom;
	[SerializeField] AudioSource[] audioTalk;
	[SerializeField] Collider2D perceptionAura;
	[SerializeField] LayerMask perceptionAuraLayerMask;

	bool isAwake;
	float nextTalk;

	// Start is called before the first frame update
	void Start() {
		nextTalk = Time.time;
		List<Collider2D> results = new List<Collider2D>();
		ContactFilter2D filter = new ContactFilter2D();
		filter.SetLayerMask(perceptionAuraLayerMask);
		perceptionAura.OverlapCollider(filter, results);
		if (results.Count > 0) {
			WakeUp();
		}
		else {
			Sleep();
		}
	}

	// Update is called once per frame
	void Update() {
		if (isAwake) {
			if (Time.time >= nextTalk) {
				Talk();
			}
		}
	}

	public EnemyStats GetStats() { return stats; }

	public void Kill() {
		GameObject lootbag;
		Instantiate(soundDeathPrefab, transform.parent);
		lootbag = Instantiate(lootBagPrefab, transform.position, transform.rotation) as GameObject;
		Destroy(gameObject);
	}

	public void WakeUp() {
		isAwake = true;
		gameObject.GetComponent<AIPath>().enabled = true;
		gameObject.GetComponent<AIDestinationSetter>().enabled = true;
	}

	public void Sleep() {
		isAwake = false;
		gameObject.GetComponent<AIPath>().enabled = false;
		gameObject.GetComponent<AIDestinationSetter>().enabled = false;
	}

	public void Talk() {
		nextTalk = Time.time + talkCooldDownMin + Random.Range(0.0f, talkCoolDownRandom);
		audioTalk[Random.Range(0, audioTalk.Length)].Play();
	}
}
