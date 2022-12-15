using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class that manages enemy spawns
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class SpawnManager : MonoBehaviour {
    // The Player Manager.
    [SerializeField] PlayerManager playerManager;
    // The parent location of the spawner instances.
	[SerializeField] Transform enemyParent;
    // The prefabs of all the enemies the can be spawned.
	[SerializeField] GameObject[] enemyPrefabs;
    // The chances of each enemy type spawning. Should match enemyPrefabs list.
	[SerializeField] int[] enemyTypeChances;
    // Minimum amount of enemies to be spawned.
    [SerializeField] int spawnAmountMin;
    // Amount of additional zombies to spawn per Player level.
    [SerializeField] int spawnAmountPerLevel;
    // Random range of extra enemies to be spawned. 0 if the number of enemies to spawn should always be the same.
    [SerializeField] int spawnAmountRandomMax;

	// Start is called before the first frame update
    /*
     * Spawns zombies at random instanciated spawner locations in the scene.
     * The number of zombies spawned is dependent on player level and a random range.
     * The level of the spawned enemies is set to be the same level as the player.
     */
	public bool Initialize() {
        Debug.Log("Initialize SpawnManager...");
        int playerLevel = playerManager.GetPlayerLevel();
        int SpawnAmount = spawnAmountMin + (playerLevel * spawnAmountPerLevel) + Random.Range(0, spawnAmountRandomMax + 1);

        // Set the level of all the prefabs to match the player's level.
        EnemyStats enemyStats;
        foreach (GameObject enemyPrefab in enemyPrefabs)
        {
            enemyStats = enemyPrefab.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.SetLevel(playerLevel);
            }
        }

        // Calculate the total number of chances between all enemy types.
        int totalEnemyTypeChances = 0;
        foreach (int chance in enemyTypeChances)
        {
            totalEnemyTypeChances += chance;
        }

        EnemySpawner[] enemySpawnerScripts = enemyParent.GetComponentsInChildren<EnemySpawner>();
        int enemySpawnerScriptsLength = enemySpawnerScripts.Length;
        if (enemySpawnerScriptsLength < SpawnAmount)
        {
            SpawnAmount = enemySpawnerScriptsLength;
        }

        // Generate a list of the random indices of the spawners on which to spawn enemies.
        List<int> spawnerIndices = new List<int>();
        int rSpawnerIndex;
        while (spawnerIndices.Count < SpawnAmount)
        {
            rSpawnerIndex = Random.Range(0, enemySpawnerScriptsLength);
            if (!(spawnerIndices.Contains(rSpawnerIndex)))
            {
                spawnerIndices.Add(rSpawnerIndex);
            }
        }

        // Spawn Enemies on the spawner who have their index in the spawnerIndices list,
        // Destroy all the spawners.
        int r; // random number which will determine the enemy type.
        EnemySpawner enemySpawnerScript;
        for (int spawnerIndex = 0; spawnerIndex < enemySpawnerScriptsLength; spawnerIndex++)
        {
            enemySpawnerScript = enemySpawnerScripts[spawnerIndex];
            if (!(spawnerIndices.Contains(spawnerIndex)))
            { // This spawner will not generate an enemy, destroy it.
                enemySpawnerScript.DestroySpawner();
            }
            else
            { // This spawner will generate an enemy, generate the type randomly.
                r = Random.Range(0, totalEnemyTypeChances);
                for (int i = 0; i < enemyTypeChances.Length; i++)
                {
                    if (r < enemyTypeChances[i])
                    { // This is the type, generate enemy and destroy spawner.
                        enemySpawnerScript.SpawnEnemy(enemyPrefabs[i]);
                        break;
                    }
                    else
                    {
                        r -= enemyTypeChances[i];
                    }
                }
            }
        }
        return true;
	}

	// Update is called once per frame
	void Update() {
      
	}
}
