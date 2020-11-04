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

	[SerializeField]
	GameObject inventory_food_slot_prefab;
	GameObject inventory_food_slot;
	Inventory player_inventory;

	// Start is called before the first frame update
	void Start() {
		player_inventory = FindObjectOfType<Player>().inventory;

		// TODO player_inventory.consumables["Food"] doesn't work. Is it because it hasn't been initialized yet?
  //      if (player_inventory.consumables["Food"] != 0)
  //      {
		//	inventory_food_slot = Instantiate(inventory_food_slot_prefab);
		//	inventory_food_slot.transform.SetParent(GameObject.FindGameObjectWithTag("InventoryContent").transform);
		//	inventory_food_slot.transform.Find("ItemCount").GetComponent<Text>().text = "x" + player_inventory.consumables["Food"];

		//}
	}

	// Update is called once per frame
	void Update() {

	}

	public void ButtonConsumeClick(string option)
    {
		Debug.Log("Consuming");
    }

	public void ButtonEquipClick(string option)
	{
		Debug.Log("Equipping");
	}

	public void ButtonDropClick(string item)
	{
		Debug.Log("Dropping");
	}

	public void ButtonDropAllClick(string item)
	{
		Debug.Log("Dropping All");
	}
}
