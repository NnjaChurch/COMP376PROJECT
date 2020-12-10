using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the inventory used by Player
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Inventory : MonoBehaviour {
	
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] PlayerManager manager_player;

	float current_weight;

	public IDictionary<string, int> consumables = new Dictionary<string, int>();
	public IDictionary<string, int> materials = new Dictionary<string, int>();

	public IDictionary<string, bool> weapons = new Dictionary<string, bool>();
	public IDictionary<string, bool> armours = new Dictionary<string, bool>();

	bool isEncumbered;

	// Start is called before the first frame update
	public void Initialize() {
		Debug.Log("Initializing Inventory...");
		current_weight = 0;
		isEncumbered = false;

		if(manager_inventory.CheckSave()) {
			List<int> inventory_load = manager_inventory.LoadInventory();

			weapons.Add("Knife", inventory_load[0] != 0);
			weapons.Add("Bat", inventory_load[1] != 0);
			weapons.Add("Shovel", inventory_load[2] != 0);
			weapons.Add("Rake", inventory_load[3] != 0);

			armours.Add("Light Armour", inventory_load[4] != 0);
			armours.Add("Medium Armour", inventory_load[5] != 0);
			armours.Add("Heavy Armour", inventory_load[6] != 0);

			consumables.Add("Food", inventory_load[7]);
			consumables.Add("Medicine", inventory_load[8]);

			materials.Add("Nails", inventory_load[9]);
			materials.Add("Wood", inventory_load[10]);
			materials.Add("Metal", inventory_load[11]);
			materials.Add("Cloth", inventory_load[12]);
		}
		else {
			InitializeInventory();
		}
		CalculateWeight();
		//manager_inventory.UpdateUIInventory();
	}

	private void InitializeInventory() {
		// Consumables and Materials have counts
		consumables.Add("Food", 0);
		consumables.Add("Medicine", 0);

		materials.Add("Nails", 0);
		materials.Add("Wood", 0);
		materials.Add("Metal", 0);
		materials.Add("Cloth", 0);

		// Weapons and Armour are either found, or not found
		weapons.Add("Knife", false);
		weapons.Add("Bat", true);
		weapons.Add("Shovel", false);
		weapons.Add("Rake", false);

		armours.Add("Light Armour", true);
		armours.Add("Medium Armour", false);
		armours.Add("Heavy Armour", false);
	}

	private void CalculateWeight() {
		current_weight = manager_inventory.CalculateWeight();
	}

	void UpdateWeight(bool add, float item_weight) {
		current_weight = add ? current_weight + item_weight : current_weight - item_weight;
		float carry_capacity = manager_inventory.GetPlayerCarryWeight();

		if (current_weight > carry_capacity) {
			isEncumbered = true;
			manager_player.SetEncumberance(1.5f);
		}
		else {
			isEncumbered = false;
			manager_player.SetEncumberance(1.0f);
		}
	}

	public void AddToInventory(string item_name) {
		int current_count;

		switch (item_name) {
			case "Food":
			case "Medicine":
				current_count = consumables[item_name];
				consumables[item_name] = current_count + 1;
				break;

			case "Nails":
			case "Wood":
			case "Metal":
			case "Cloth":
				current_count = materials[item_name];
				materials[item_name] = current_count + 1;
				break;

			case "Bat":
			case "Knife":
			case "Shovel":
			case "Rake":
				weapons[item_name] = true;
				break;


			case "Light Armour":
			case "Medium Armour":
			case "Heavy Armour":
				armours[item_name] = true;
				break;
		}

		CalculateWeight();
		manager_inventory.UpdateUIInventory();
	}

	public void RemoveFromInventory(string item_name) {
		int current_count;
		switch (item_name) {
			case "Food":
			case "Medicine":
				current_count = consumables[item_name];
				consumables[item_name] = current_count - 1;
				break;

			case "Nails":
			case "Wood":
			case "Metal":
			case "Cloth":
				current_count = materials[item_name];
				materials[item_name] = current_count - 1;
				break;
		}

		CalculateWeight();
		manager_inventory.UpdateUIInventory();
	}

	public void UseMaterial(string material_name, int count) {
		materials[material_name] -= count;
		CalculateWeight();
		manager_inventory.UpdateUIInventory();
	}

	public float GetInventoryWeight() {
		return current_weight;
	}

	public bool CheckEncumberance() {
		return isEncumbered;
	}

}
