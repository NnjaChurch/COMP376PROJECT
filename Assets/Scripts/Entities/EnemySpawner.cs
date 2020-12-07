using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int[] chances;

    // Start is called before the first frame update
    void Start()
    {
        int total_chances = 0;
        foreach (int chance in chances)
        {
            total_chances += chance;
        }
        float r = Random.Range(0.0f, total_chances);
        for (int i = 0; i < chances.Length; i++)
        {
            if (r < chances[i])
            {
                Instantiate(enemyPrefabs[i], gameObject.transform.position, gameObject.transform.rotation, gameObject.transform.parent);
                break;
            }
            else 
            {
                r -= chances[i];
            }
        }
        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
