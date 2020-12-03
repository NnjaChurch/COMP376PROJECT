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

	// TODO: Its an extra step, but it guarantees that nothing breaks if you change individual parts (only have to update functions when the UIManager is changed)
	// TODO: Update the way data is passed to InventoryUI via the UIManager

	[SerializeField] UIManager manager_UI;

	[SerializeField] GameObject inventory_content; // Reference to 'Content' gameObject under InventoryPanel
	[SerializeField] Text inventory_weight_text; // Text that shows current weight in the bottom of inventory

	GameObject items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	public static bool inventory_updated = false; // To know if there is a change in inventory

	// Start is called before the first frame update
	void Start() {

		items = manager_UI.GetPrefabItems();

		for (int i = 0; i < inventory_content.transform.childCount; i++)
        {
			inventory_content.transform.GetChild(i).gameObject.SetActive(false);
        }

		this.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update() {
		if (inventory_updated) // Refresh the inventory UI if there is a change in inventory
        {
			updateInventoryUI();
			inventory_updated = false; // Re set to false as we just updated the inventory UI
		}
	}

	public void ButtonInventoryClick()
    {
		Debug.Log("InventoryUI.ButtonInventoryClick()");

		if (!this.gameObject.activeSelf)
		{
			this.gameObject.SetActive(true);
		}
		else
		{
			this.gameObject.SetActive(false);
		}
	}

	public void ButtonConsumeClick(string consumable)
    {
		Debug.Log("InventoryUI.ButtonConsumeClick(): " + consumable);
		manager_UI.Consume(consumable);
    }

	public void ButtonEquipWeapon(string weapon)
	{
		Debug.Log("InventoryUI.ButtonEquipWeapon(): " + weapon);

		manager_UI.EquipWeapon(weapon);
		manager_UI.UpdateInventoryUI();
		manager_UI.UpdatePlayerEquippedWeapon();
	}

	public void ButtonEquipArmour(string armour)
	{
		Debug.Log("InventoryUI.ButtonEquipArmour(): " + armour);

		manager_UI.EquipArmour(armour);
		manager_UI.UpdateInventoryUI();
		manager_UI.UpdatePlayerEquippedArmour();
	}

	public void ButtonDropConsumable(string item)
	{
		string name = item.Substring(0, item.Length-1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			Debug.Log("InventoryUI.ButtonDropConsumable(): " + name);
			manager_UI.RemoveFromInventory(name);
		}
		else // Drop all
        {
			Debug.Log("InventoryUI.ButtonDropAllConsumable(): " + name);
			int consumable_count = manager_UI.GetConsumables()[name];
			for (int i = 0; i < consumable_count; i++)
            {
				manager_UI.RemoveFromInventory(name);
			}
        }
	}

	public void ButtonDropMaterial(string item)
	{
		string name = item.Substring(0, item.Length - 1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			Debug.Log("InventoryUI.ButtonDropMaterial(): " + name);
			manager_UI.RemoveFromInventory(name);
		}
		else // Drop all
		{
			int material_count = manager_UI.GetMaterials()[name];
			for (int i = 0; i < material_count; i++)
			{
				Debug.Log("InventoryUI.ButtonDropAllMaterial(): " + name);
				manager_UI.RemoveFromInventory(name);
			}
		}
	}


	public void updateInventoryUI()
    {
		Debug.Log("InventoryUI.updateInventoryUI()");

		Transform itemSlot; // To get reference to each of the inventory UI slots
		IDictionary<string, int> dictionary = null;

		for (int i = 0; i < 4; i++) // 4 times since inventory has 4 dictionaries
		{
			switch (i)
			{
				case 0:
					dictionary = manager_UI.GetConsumables();
					break;
				case 1:
					dictionary = manager_UI.GetMaterials();
					break;
				case 2:
					dictionary = manager_UI.GetFoundWeapons();
					break;
				case 3:
					dictionary = manager_UI.GetFoundArmour();
					break;
			}

			foreach (KeyValuePair<string, int> entry in dictionary)
			{
				itemSlot = inventory_content.transform.Find(entry.Key);
				if (entry.Value != 0)
				{
					int itemWeight = -1;
					string itemDescription = "N/A";

					if (i == 0) // Dealing with consumables
					{
						Consumable c = items.transform.Find(entry.Key).GetComponent<Consumable>();
						itemWeight = dictionary[entry.Key] * (int)c.GetWeight();
						itemDescription = "+" + c.GetHPGain() + " Health";
					}
					else if (i == 1) // Dealing with materials
					{
						Material m = items.transform.Find(entry.Key).GetComponent<Material>();
						itemWeight = dictionary[entry.Key] * (int)m.GetWeight();
						itemDescription = "Used for upgrades";
					}
					else if (i == 2) { // Dealing with weapons
						itemWeight = dictionary[entry.Key] * manager_UI.GetWeaponWeight(entry.Key);
						itemDescription = "Damage: " + manager_UI.GetWeaponDamage(entry.Key);
					}
					else if (i == 3) // Dealing with armours
					{
						itemWeight = dictionary[entry.Key] * manager_UI.GetArmourWeight(entry.Key);
						itemDescription = "Defense: " + manager_UI.GetArmourDefense(entry.Key);
					}

					// ------------------------------------------------------------------------------------------------------//

					if (itemSlot.gameObject.activeSelf) // item had more than 1 count already, and increased or decreased but still has more than 1
					{
						itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + dictionary[entry.Key];
						itemSlot.Find("ItemWeight").GetComponent<Text>().text = itemWeight + " lbs";
						itemSlot.Find("ItemDescription").GetComponent<Text>().text = itemDescription;
					}
					else // if item had 0 but now has more than 1
					{
						itemSlot.gameObject.SetActive(true);
						itemSlot.Find("ItemCount").GetComponent<Text>().text = "x" + dictionary[entry.Key];
						itemSlot.Find("ItemWeight").GetComponent<Text>().text = itemWeight + " lbs";
						itemSlot.Find("ItemDescription").GetComponent<Text>().text = itemDescription;
						itemSlot.SetAsLastSibling();
					}
				}
				else // if item count is 0
				{
					itemSlot.gameObject.SetActive(false);
				}
			}
		}

		inventory_weight_text.text = "Weight: " + manager_UI.GetWeight() + " / " + (int)manager_UI.GetMaxWeight() + " lbs";
	}
}
