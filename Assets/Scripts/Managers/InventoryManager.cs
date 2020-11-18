using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField] Inventory player_inventory;
    [SerializeField] EquipmentManager equipment_manager;
    [SerializeField] PlayerManager player_manager;
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveInventory()
    {

    }
    public void LoadInventory()
    {

    }

    public void RemoveFromInventory(string itemName)
    {
        player_inventory.RemoveFromInventory(itemName);
    }

    public void AddToInventory(string itemName)
    {
        player_inventory.AddToInventory(itemName);
    }

    public int GetWeight()
    {
        return (int) player_inventory.GetInventoryWeight();
    }

    public IDictionary<string, int> GetConsumables()
    {
        return player_inventory.consumables;
    }

    public IDictionary<string, int> GetMaterials()
    {
        return player_inventory.materials;
    }

    public IDictionary<string, int> GetFoundWeapons()
    {
        IDictionary<string, int> weaponsCount = new Dictionary<string, int>();
        foreach(KeyValuePair<string, bool> weapon in player_inventory.weapons)
        {
            // if the weapon has been found (ie. is true) and is not currently equipped, return to UI
            if (weapon.Value && equipment_manager.GetEquippedWeapon().name != weapon.Key) { weaponsCount.Add(weapon.Key, 1); }
        }
        return weaponsCount;
    }

    public IDictionary<string, int> GetFoundArmour()
    {
        IDictionary<string, int> armourCount = new Dictionary<string, int>();
        foreach (KeyValuePair<string, bool> armour in player_inventory.armours)
        {
            // if the armour has been found (ie. is true) and is not currently equipped, return to UI
            if (armour.Value && equipment_manager.GetEquippedArmour().name != armour.Key) { armourCount.Add(armour.Key, 1); }
        }
        return armourCount;
    }

    public void consume(string consumable)
    {
        RemoveFromInventory(consumable);
        player_manager.consume(consumable);
    }
}
