using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the looting window when interaction with lootable objects
//	Contributors: Rubiat
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class LootMenu : MonoBehaviour {
	// Start is called before the first frame update

	[SerializeField] InventoryManager inventory_manager;
	[SerializeField] GameObject loot_content; // Reference to 'Content' gameObject under LootMenu

	Lootbag current_lootbag; // To know which lootbag is currently opened

	void Start() {
		ResetLootUI();
		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {

	}

	public void DisplayLoot(Lootbag loot_bag)
    {
		Debug.Log("LootMenu.DisplayLoot()");

		current_lootbag = loot_bag;
		ResetLootUI();
		Transform itemSlot; // To get reference to each of the loot UI slots

		List<LootUIEntity> loot_items = loot_bag.GetItems();

		for (int i = 0; i < loot_items.Count; i++)
        {
			itemSlot = loot_content.transform.Find(loot_items[i].item_name);
			itemSlot.gameObject.SetActive(true);
			// TODO item weight

			string count = itemSlot.Find("ItemCount").GetComponent<Text>().text.Substring(1); // The actual string is "x##"
			int itemCount = int.Parse(count);
			itemCount++;
			itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + itemCount;
		}

		this.gameObject.SetActive(true);
		// TODO when to SetActive(false)?
    }

	// Reset loot ui everytime we open the loot ui, so that we don't accidentally show the loot from another lootbag
	public void ResetLootUI()
    {
		for (int i = 0; i < loot_content.transform.childCount; i++)
		{
			Transform itemSlot = loot_content.transform.GetChild(i);
			itemSlot.Find("ItemCount").GetComponent<Text>().text = "x0";
			itemSlot.gameObject.SetActive(false);
		}
	}

	public void ButtonPickUp(string item)
    {
		Debug.Log("LootMenu.ButtonPickUp(): " + item);

		inventory_manager.AddToInventory(item);
		List<LootUIEntity> loot_items = current_lootbag.GetItems();

		for (int i = 0; i < loot_items.Count; i++)
        {
			if (loot_items[i].item_name == item)
            {
				loot_items.RemoveAt(i);
				break;
            }
        }

		DisplayLoot(current_lootbag);
    }

	public void ButtonPickUpAll(string item)
    {
		Debug.Log("LootMenu.ButtonPickUpAll(): " + item);

		List<LootUIEntity> loot_items = current_lootbag.GetItems();

		for (int i = loot_items.Count - 1; i >= 0; i--)
        {
			if (loot_items[i].item_name == item)
			{
				inventory_manager.AddToInventory(item);
				loot_items.RemoveAt(i);
			}
        }

		DisplayLoot(current_lootbag);
	}
}
