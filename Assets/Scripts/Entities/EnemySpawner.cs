using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public void SpawnEnemy(GameObject enemyPrefab) {
		Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
		DestroySpawner();

	}

	public void DestroySpawner() {
		GameObject.Destroy(gameObject);
	}
}
