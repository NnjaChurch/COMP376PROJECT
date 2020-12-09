using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnManager : MonoBehaviour
{
    GameObject[] dressers;
    GameObject[] vehicles;

    float dresser_rate = 0.5f;
    float vehicle_rate = 0.25f;

    public bool Initialize()
    {
        dressers = GameObject.FindGameObjectsWithTag("Lootable_dresser");
        RandomSpawnLootbags(dressers, dresser_rate);

        vehicles = GameObject.FindGameObjectsWithTag("Lootable_vehicle");
        RandomSpawnLootbags(vehicles, vehicle_rate);
        return true;
    }

    void RandomSpawnLootbags(GameObject[] collection, float spawn_rate)
    {
        foreach(GameObject container in collection)
        {
            float random = Random.Range(0.0f, 1.0f);
            if (random <= spawn_rate)
            {
                container.AddComponent<Lootbag>();
            }
        }
    }
}
