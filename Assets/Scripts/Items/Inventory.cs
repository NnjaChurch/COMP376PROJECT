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
    [SerializeField] UIManager UI_manager;

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
        weapons.Add("Shovel", false);
        weapons.Add("Rake", false);

        armours.Add("Light Armour", true);
        armours.Add("Medium Armour", false);
        armours.Add("Heavy Armour", false);

        UI_manager.updateInventoryUI();
	}

	// Update is called once per frame
	void Update()	{

	}

	void UpdateWeight(bool add, float itemWeight)
	{
		current_weight = add ? current_weight + itemWeight : current_weight - itemWeight;
		Player player = gameObject.GetComponentInParent<Player>();
		float carry_capacity = 0; //player.GetCarryWeight();

        if (current_weight > carry_capacity)
        {
            isEncumbered = true;
        }
        else {
            isEncumbered = false;
        }
    }
    public void AddToInventory(string itemName)
    {
        int currentCount;
        switch (itemName)
        {
            case "Food":
            case "Medicine":
                currentCount = consumables[itemName];
                consumables[itemName] = currentCount + 1;
                break;

            case "Nails":
                break;
            case "Wood":
                break;
            case "Metal":
                break;
            case "Cloth":
                currentCount = materials[itemName];
                materials[itemName] = currentCount + 1;
                break;

            case "Bat":
            case "Knife":
            case "Shovel":
            case "Rake":
                weapons[itemName] = true;
                break;


            case "Light Armour":
            case "Medium Armour":
            case "Heavy Armour":
                armours[itemName] = true;
                break;
        }

        UI_manager.updateInventoryUI();
        //TODO: The inventory weight needs to be updated
    }
    public void RemoveFromInventory(string itemName)
    {
        int currentCount;
        switch (itemName)
        {
            case "Food":
            case "Medicine":
                currentCount = consumables[itemName];
                consumables[itemName] = currentCount - 1;
                break;

            case "Nails":
                break;
            case "Wood":
                break;
            case "Metal":
                break;
            case "Cloth":
                currentCount = materials[itemName];
                materials[itemName] = currentCount - 1;
                break;
        }

        UI_manager.updateInventoryUI();
        //TODO: The inventory weight needs to be updated
    }

    public float GetInventoryWeight() { return current_weight; }
	
}
