using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the Inventory User Interface
//	Contributors: Kevin, Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class InventoryUI : MonoBehaviour {

	[SerializeField] Inventory player_inventory;
	Player player;

	GameObject inventory_content; // Reference to 'Content' gameObject under InventoryPanel
	[SerializeField]
	GameObject inventory_weight_text; // Text that shows current weight in the bottom of inventory

	public static bool inventory_updated = false; // To know if there is a change in inventory

	// Start is called before the first frame update
	void Start() {
		player = FindObjectOfType<Player>();
		//    inventory_food_slot.transform.Find("ItemCount").GetComponent<Text>().text = "x" + player_inventory.consumables["Food"];

		inventory_content = GameObject.FindGameObjectWithTag("InventoryContent");
		for (int i = 0; i < inventory_content.transform.childCount; i++)
        {
			inventory_content.transform.GetChild(i).gameObject.SetActive(false);
        }
	}

	// Update is called once per frame
	void Update() {
		if (inventory_updated) // Refresh the inventory UI if there is a change in inventory
        {
			updateInventoryUI();
			//inventory_weight_text.GetComponent<Text>().text = "Weight: " + player_inventory.GetInventoryWeight() + "/" + player.GetCarryWeight() + " lbs";
			inventory_updated = false; // Re set to false as we just updated the inventory UI
		}
	}

	public void ButtonConsumeClick(string option)
    {
		Debug.Log("Consuming");
    }

	public void ButtonEquipWeapon(string item)
	{
		// TODO Potential recipe for disaster here. I'm just creating a weapon without any stats, so it will equip a faulty weapon
		Weapon w = new Weapon();
		w.name = item;
		//player.EquipWeapon(w);
	}

	public void ButtonEquipArmour(string item)
	{
		// TODO Potential recipe for disaster here. I'm just creating a armour without any stats, so it will equip a faulty armour
		Armour a = new Armour();
		a.name = item;
		//player.EquipArmour(a);
	}

	public void ButtonDropConsumable(string item)
	{
		// TODO Creating a temporary Consumable might not be the right way to do this
		string name = item.Substring(0, item.Length-1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		Consumable c = new Consumable();
		c.consumable_name = name;

		if (dropAll == 0) // Drop one
		{
			player_inventory.RemoveFromInventory(c);
		}
		else // Drop all
        {
			int consumable_count = player_inventory.consumables[name];
			for (int i = 0; i < consumable_count; i++)
            {
				player_inventory.RemoveFromInventory(c);
			}
        }
	}

	public void ButtonDropMaterial(string item)
	{
		// TODO Creating a temporary Consumable might not be the right way to do this
		string name = item.Substring(0, item.Length - 1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		Material m = new Material();
		m.material_name = name;

		if (dropAll == 0) // Drop one
		{
			player_inventory.RemoveFromInventory(m);
		}
		else // Drop all
		{
			int material_count = player_inventory.materials[name];
			for (int i = 0; i < material_count; i++)
			{
				player_inventory.RemoveFromInventory(m);
			}
		}
	}


	public void updateInventoryUI()
    {
		Transform itemSlot; // To get reference to each of the inventory UI slots
		IDictionary<string, int> dictionary = null;

		for (int i = 0; i < 4; i++) // 4 times since inventory has 4 dictionaries
		{
			switch (i)
			{
				case 0:
					dictionary = player_inventory.consumables;
					break;
				case 1:
					dictionary = player_inventory.materials;
					break;
				// TODO: rework this, since weapons and armour are either found or not found (no longer countable)
				//case 2:
				//	dictionary = player_inventory.weapons;
				//	break;
				//case 3:
				//	dictionary = player_inventory.armours;
				//	break;
			}

			foreach (KeyValuePair<string, int> entry in dictionary)
			{
				itemSlot = inventory_content.transform.Find(entry.Key);
				if (entry.Value != 0)
				{
					//int itemWeight = dictionary[entry.Key] * weightOfItem;
					if (itemSlot.gameObject.activeSelf) // item had more than 1 count, and increased or decreased but still has more than 1
					{
						itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + dictionary[entry.Key];
						//itemSlot.Find("ItemWeight").GetComponent<Text>().text = itemWeight + " lbs";
					}
					else // if item had 0 but now has more than 1
					{
						itemSlot.gameObject.SetActive(true);
						itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + dictionary[entry.Key];
						//itemSlot.Find("ItemWeight").GetComponent<Text>().text = itemWeight + " lbs";
						itemSlot.SetAsLastSibling();
					}
				}
				else // if item count is 0
				{
					itemSlot.gameObject.SetActive(false);
				}
			}
		}
	}
}
