using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeZoneInventoryUI : MonoBehaviour {
	[SerializeField] UIManager manager_UI;

	[SerializeField] GameObject inventory_content; // Reference to 'Content' gameObject under InventoryPanel
	[SerializeField] Text inventory_weight_text; // Text that shows current weight in the bottom of inventory

	GameObject items; // Reference to the 'Items' gameobject that has Food, Medicine, Wood, etc.

	// Start is called before the first frame update
	void Start() {
		Debug.Log("SafeZoneInventoryUI");

		items = manager_UI.GetPrefabItems();

		for (int i = 0; i < inventory_content.transform.childCount; i++) {
			inventory_content.transform.GetChild(i).gameObject.SetActive(false);
		}

		updateInventoryUI();
	}

	public void ButtonConsumeClick(string consumable) {
		Debug.Log("SafeZoneInventoryUI.ButtonConsumeClick(): " + consumable);
		manager_UI.Consume(consumable);
		updateInventoryUI();
	}

	public void ButtonEquipWeapon(string weapon) {
		Debug.Log("SafeZoneInventoryUI.ButtonEquipWeapon(): " + weapon);

		manager_UI.EquipWeapon(weapon);
		updateInventoryUI();
		manager_UI.UpdatePlayerEquippedWeapon();
	}

	public void ButtonEquipArmour(string armour) {
		Debug.Log("SafeZoneInventoryUI.ButtonEquipArmour(): " + armour);

		manager_UI.EquipArmour(armour);
		updateInventoryUI();
		manager_UI.UpdatePlayerEquippedArmour();
	}

	public void ButtonDropConsumable(string item) {
		string name = item.Substring(0, item.Length - 1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			Debug.Log("SafeZoneInventoryUI.ButtonDropConsumable(): " + name);
			manager_UI.RemoveFromInventory(name);
		}
		else // Drop all
		{
			Debug.Log("SafeZoneInventoryUI.ButtonDropAllConsumable(): " + name);
			int consumable_count = manager_UI.GetConsumables()[name];
			for (int i = 0; i < consumable_count; i++) {
				manager_UI.RemoveFromInventory(name);
			}
		}

		updateInventoryUI();
	}

	public void ButtonDropMaterial(string item) {
		string name = item.Substring(0, item.Length - 1);
		int dropAll = int.Parse(item.Substring(item.Length - 1));

		if (dropAll == 0) // Drop one
		{
			Debug.Log("SafeZoneInventoryUI.ButtonDropMaterial(): " + name);
			manager_UI.RemoveFromInventory(name);
		}
		else // Drop all
		{
			int material_count = manager_UI.GetMaterials()[name];
			for (int i = 0; i < material_count; i++) {
				Debug.Log("SafeZoneInventoryUI.ButtonDropAllMaterial(): " + name);
				manager_UI.RemoveFromInventory(name);
			}
		}

		updateInventoryUI();
	}


	public void updateInventoryUI() {
		Debug.Log("SafeZoneInventoryUI.updateInventoryUI()");

		Transform itemSlot; // To get reference to each of the inventory UI slots
		IDictionary<string, int> dictionary = null;

		for (int i = 0; i < 4; i++) // 4 times since inventory has 4 dictionaries
		{
			switch (i) {
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

			foreach (KeyValuePair<string, int> entry in dictionary) {
				itemSlot = inventory_content.transform.Find(entry.Key);
				if (entry.Value != 0) {
					float itemWeight = -1;
					string itemDescription = "N/A";

					bool showItem = true; // Will be false if if it's a weapon or armour that is currently equipped

					if (i == 0) // Dealing with consumables
					{
						Consumable c = items.transform.Find(entry.Key).GetComponent<Consumable>();
						itemWeight = entry.Value * c.GetWeight();
						itemDescription = "+" + c.GetHPGain() + " Health";
					}
					else if (i == 1) // Dealing with materials
					{
						Material m = items.transform.Find(entry.Key).GetComponent<Material>();
						itemWeight = entry.Value * m.GetWeight();
						itemDescription = "Used for upgrades";
					}
					else if (i == 2) { // Dealing with weapons
						itemWeight = dictionary[entry.Key] * manager_UI.GetWeaponWeight(entry.Key);
						itemDescription = "Damage: " + manager_UI.GetWeaponDamage(entry.Key);
						if (manager_UI.GetEquippedWeapon().GetWeaponName() == entry.Key) {
							showItem = false;
						}
					}
					else if (i == 3) // Dealing with armours
					{
						itemWeight = dictionary[entry.Key] * manager_UI.GetArmourWeight(entry.Key);
						itemDescription = "Defense: " + manager_UI.GetArmourDefense(entry.Key);
						if (manager_UI.GetEquippedArmour().GetArmourName() == entry.Key) {
							showItem = false;
						}
					}

					// ------------------------------------------------------------------------------------------------------//

					if (showItem) {
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
					else {
						itemSlot.gameObject.SetActive(false);
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
