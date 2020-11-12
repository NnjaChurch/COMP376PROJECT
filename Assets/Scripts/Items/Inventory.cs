using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the inventory used by Player
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Inventory : MonoBehaviour
{
    float current_weight;
    bool isEncumbered;

    public IDictionary<string, int> consumables = new Dictionary<string, int>();
    public IDictionary<string, int> materials = new Dictionary<string, int>();

    public IDictionary<string, bool> weapons = new Dictionary<string, bool>();
    public IDictionary<string, bool> armours = new Dictionary<string, bool>();

    // Start is called before the first frame update
    void Start()
    {
        current_weight = 0;
        isEncumbered = false;

        consumables.Add("Food", 1);
        consumables.Add("Medicine", 0);

        materials.Add("Nails", 0);
        materials.Add("Wood", 0);
        materials.Add("Metal", 0);
        materials.Add("Cloth", 2);

        // Weapons and Armour are either found, or not found
        weapons.Add("Bat", true);
        weapons.Add("Knife", false);
        weapons.Add("Sword", false);
        weapons.Add("Rake", false);

        armours.Add("Light", true);
        armours.Add("Medium", false);
        armours.Add("Heavy", false);

        InventoryUI.inventory_updated = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateWeight(bool add, float itemWeight)
    {
        current_weight = add ? current_weight + itemWeight : current_weight - itemWeight;
        Player player = gameObject.GetComponentInParent<Player>();
        float carry_capacity = player.GetCarryWeight();

        if (current_weight > carry_capacity)
        {
            isEncumbered = true;
        }
        else {
            isEncumbered = false;
        }
    }
    public void AddToInventory(Item i)
    {
        int currentCount;
        switch (i)
        {
            case Consumable c:
                currentCount = consumables[c.GetConsumableName()];
                consumables[c.GetConsumableName()] = currentCount + 1;
                break;
            case Material m:
                currentCount = materials[m.GetMaterialName()];
                materials[m.GetMaterialName()] = currentCount + 1;
                break;
            case Weapon w:
                weapons[w.GetWeaponName()] = true;
                break;
            case Armour a:
                armours[a.GetArmourName()] = true;
                break;
        }

        InventoryUI.inventory_updated = true; // So that inventory UI updates during next Update()
        UpdateWeight(true, i.GetWeight());
    }
    public void RemoveFromInventory(Item i)
    {
        int currentCount;
        switch (i)
        {
            case Consumable c:
                currentCount = consumables[c.GetConsumableName()];
                consumables[c.GetConsumableName()] = currentCount - 1;
                break;
            case Material m:
                currentCount = materials[m.GetMaterialName()];
                materials[m.GetMaterialName()] = currentCount - 1;
                break;
        }

        InventoryUI.inventory_updated = true; // So that inventory UI updates during next Update()
        UpdateWeight(false, i.GetWeight());
    }

    public float GetInventoryWeight() { return current_weight; }
    
}
