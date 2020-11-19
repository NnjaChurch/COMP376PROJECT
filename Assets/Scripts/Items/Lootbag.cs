using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour
{
    // Start is called before the first frame update
    LootManager loot_manager;

    // bag, vehicle, furnite
    [SerializeField] string item_type;

    List<LootUIEntity> items = new List<LootUIEntity>();

    void Start()
    {
        loot_manager = GameObject.Find("LootManager").GetComponent<LootManager>();
        LootUIEntity item = loot_manager.GenerateLootForUI(item_type);
        items.Add(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<LootUIEntity> GetItems()
    {
        return items;
    }

    public void ResetItems()
    {
        items.Clear();
        LootUIEntity item = loot_manager.GenerateLootForUI(item_type);
        items.Add(item);
    }
}
