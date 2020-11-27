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

	// TODO: Make sure to only have InventoryUI communicate with UIManager and not the other managers (add functions in UIManager that call functions in Inventory / EquipmentManagers and not a direct link)
	// TODO: Its an extra step, but it guarantees that nothing breaks if you change individual parts (only have to update functions when the UIManager is changed)
	// TODO: Update the way data is passed to InventoryUI via the UIManager

	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] UIManager manager_UI;

	[SerializeField] GameObject inventory_content; // Reference to 'Content' gameObject under InventoryPanel
	[SerializeField] Text inventory_weight_text; // Text that shows current weight in the bottom of inventory

	GameObject items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	public static bool inventory_updated = false; // To know if there is a change in inventory

	// Start is called before the first frame update
	void Start() {

		items = ui_manager.GetPrefabItems();

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

	public void ButtonConsumeClick(string consumable)
    {
		manager_inventory.Consume(consumable);
    }

	public void ButtonEquipWeapon(string weapon)
	{
		manager_equipment.EquipWeapon(weapon);
		manager_UI.UpdateInventoryUI();
		manager_UI.UpdatePlayerEquippedWeapon();
	}

	public void ButtonEquipArmour(string armour)
	{
		manager_equipment.EquipArmour(armour);
		manager_UI.UpdateInventoryUI();
		manager_UI.UpdatePlayerEquippedArmour();
	}

	public void ButtonDropConsumable(string item)
	{
		string name = item.Substring(0, item.Length-1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			manager_inventory.RemoveFromInventory(name);
		}
		else // Drop all
        {
			int consumable_count = manager_inventory.GetConsumables()[name];
			for (int i = 0; i < consumable_count; i++)
            {
				manager_inventory.RemoveFromInventory(name);
			}
        }
	}

	public void ButtonDropMaterial(string item)
	{
		string name = item.Substring(0, item.Length - 1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			manager_inventory.RemoveFromInventory(name);
		}
		else // Drop all
		{
			int material_count = manager_inventory.GetMaterials()[name];
			for (int i = 0; i < material_count; i++)
			{
				manager_inventory.RemoveFromInventory(name);
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
					dictionary = manager_inventory.GetConsumables();
					break;
				case 1:
					dictionary = manager_inventory.GetMaterials();
					break;
				case 2:
					dictionary = manager_inventory.GetFoundWeapons();
					break;
				case 3:
					dictionary = manager_inventory.GetFoundArmour();
					break;
			}

			foreach (KeyValuePair<string, int> entry in dictionary)
			{
				itemSlot = inventory_content.transform.Find(entry.Key);
				if (entry.Value != 0)
				{
					// TODO So far only the weights of weapons and armours can be obtained this way. Should we have references to consumables and material in equipment manager too?
					int itemWeight = -1;

					if (i == 2) { // Dealing with weapons
						itemWeight = dictionary[entry.Key] * manager_equipment.GetWeaponWeight(entry.Key);
					}
					else if (i == 3)
                    {
						itemWeight = dictionary[entry.Key] * manager_equipment.GetArmourWeight(entry.Key);
					}

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

		inventory_weight_text.text = "Weight: " + manager_inventory.GetWeight() + " / 100 lbs";
	}
}
