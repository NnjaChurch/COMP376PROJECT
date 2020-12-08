using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
        DestroySpawner();

    }

    public void DestroySpawner()
    {
        GameObject.Destroy(gameObject);
    }
}
