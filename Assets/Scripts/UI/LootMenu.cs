using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the looting window when interaction with lootable objects
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class LootMenu : MonoBehaviour {
	// Start is called before the first frame update

	[SerializeField] GameObject loot_content; // Reference to 'Content' gameObject under LootMenu

	void Start() {
		ResetLootUI();
	}

	// Update is called once per frame
	void Update() {

	}

	public void DisplayLoot(Lootbag loot_bag)
    {
		ResetLootUI();
		Transform itemSlot; // To get reference to each of the loot UI slots

		List<LootUIEntity> loot_items = loot_bag.GetItems();

		for (int i = 0; i < loot_items.Count; i++)
        {
			itemSlot = loot_content.transform.Find(loot_items[i].item_name);
			itemSlot.gameObject.SetActive(true);
			// TODO item weight
			// TODO item count
			//itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + loot_items[i].item_count;
		}

		this.gameObject.SetActive(true);
		// TODO when to SetActive(false)?
    }

	// Reset loot ui everytime we open the loot ui, so that we don't accidentally show the loot from another lootbag
	public void ResetLootUI()
    {
		for (int i = 0; i < loot_content.transform.childCount; i++)
		{
			loot_content.transform.GetChild(i).gameObject.SetActive(false);
		}
	}
}
