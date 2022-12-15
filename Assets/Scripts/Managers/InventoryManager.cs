using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

	// Inventory Reference
	[SerializeField] Inventory player_inventory;

	// Manager References
	[SerializeField] EquipmentManager manager_equipment;
	[SerializeField] PlayerManager manager_player;
	[SerializeField] UIManager manager_ui;
	[SerializeField] SaveManager manager_save;

	// Item Prefab References
	[SerializeField] Consumable consumable_food;
	[SerializeField] Consumable consumable_medicine;
	[SerializeField] Material material_wood;
	[SerializeField] Material material_nails;
	[SerializeField] Material material_cloth;
	[SerializeField] Material material_metal;

	public bool Initialize() {
		Debug.Log("Initializing InventoryManager...");
		player_inventory.Initialize();
		return true;
	}

	public List<int> SaveInventory() {
		List<int> inventory_save = new List<int>();

		// Weapons
		foreach (KeyValuePair<string, bool> weapon in player_inventory.weapons) {
			if (weapon.Value) {
				inventory_save.Add(1);
			}
			else {
				inventory_save.Add(0);
			}
		}
		// Armours
		foreach (KeyValuePair<string, bool> armour in player_inventory.armours) {
			if (armour.Value) {
				inventory_save.Add(1);
			}
			else {
				inventory_save.Add(0);
			}
		}
		// Consumables
		foreach (KeyValuePair<string, int> consumable in player_inventory.consumables) {
			inventory_save.Add(consumable.Value);
		}
		// Materials
		foreach (KeyValuePair<string, int> material in player_inventory.materials) {
			inventory_save.Add(material.Value);
		}

		return inventory_save;
	}

	public float CalculateWeight() {
		float weight = 0;

		// Weapons
		foreach (KeyValuePair<string, bool> weapon in player_inventory.weapons) {
			if (weapon.Value) {
				weight += manager_equipment.GetWeaponWeight(weapon.Key);
			}
		}
		// Armours
		foreach (KeyValuePair<string, bool> armour in player_inventory.armours) {
			if (armour.Value) {
				weight += manager_equipment.GetArmourWeight(armour.Key);
			}
		}
		// Consumables
		foreach (KeyValuePair<string, int> consumable in player_inventory.consumables) {
			if (consumable.Key.Equals(consumable_food.GetConsumableName())) {
				weight += consumable.Value * consumable_food.GetWeight();
			}
			if (consumable.Key.Equals(consumable_medicine.GetConsumableName())) {
				weight += consumable.Value * consumable_medicine.GetWeight();
			}
		}
		// Materials
		foreach (KeyValuePair<string, int> material in player_inventory.materials) {
			if (material.Key.Equals(material_nails.GetMaterialName())) {
				weight += material.Value * material_nails.GetWeight();
			}
			if (material.Key.Equals(material_wood.GetMaterialName())) {
				weight += material.Value * material_wood.GetWeight();
			}
			if (material.Key.Equals(material_metal.GetMaterialName())) {
				weight += material.Value * material_metal.GetWeight();
			}
			if (material.Key.Equals(material_cloth.GetMaterialName())) {
				weight += material.Value * material_cloth.GetWeight();
			}
		}

		return weight;
	}

	public void RemoveFromInventory(string item_name) {
		player_inventory.RemoveFromInventory(item_name);
	}

	public void PurgeItem(string item_name) {
		player_inventory.PurgeItem(item_name);
	}

	public void RemoveAllMaterialsAndConsumables() {
		List<string> item_purge = new List<string>();
		IDictionary<string, int> consumables = GetConsumables();
		IDictionary<string, int> materials = GetMaterials();
		foreach (KeyValuePair<string, int> consumable in consumables) {
			if(consumable.Value > 0) {
				item_purge.Add(consumable.Key);
			}
		}
		foreach (KeyValuePair<string, int> material in materials) {
			if(material.Value > 0) {
				item_purge.Add(material.Key);
			}
		}

		foreach(string key in item_purge) {
			PurgeItem(key);
		}
	}

	public void AddToInventory(string item_name) {
		player_inventory.AddToInventory(item_name);
	}

	public float GetWeight() {
		return player_inventory.GetInventoryWeight();
	}

	public IDictionary<string, int> GetConsumables() {
		return player_inventory.consumables;
	}

	public IDictionary<string, int> GetMaterials() {
		return player_inventory.materials;
	}

	public IDictionary<string, int> GetFoundWeapons() {
		IDictionary<string, int> weaponsCount = new Dictionary<string, int>();
		foreach (KeyValuePair<string, bool> weapon in player_inventory.weapons) {
			// if the weapon has been found (ie. is true) and is not currently equipped, return to UI
			// NOTE: This was changed to return any found weapons to ui, even if it's equipped. The check to see if it's equipped will be done in ui script
			if (weapon.Value) { weaponsCount.Add(weapon.Key, 1); }
		}
		return weaponsCount;
	}

	public IDictionary<string, int> GetFoundArmour() {
		IDictionary<string, int> armourCount = new Dictionary<string, int>();
		foreach (KeyValuePair<string, bool> armour in player_inventory.armours) {
			// if the armour has been found (ie. is true) and is not currently equipped, return to UI
			// NOTE: This was changed to return any found armours to ui, even if it's equipped. The check to see if it's equipped will be done in ui script
			if (armour.Value) { armourCount.Add(armour.Key, 1); }
		}
		return armourCount;
	}

	public void Consume(string consumable) {
		// Check consumable name and get healing value, then pass to PlayerManager
		if (consumable.Equals(consumable_food.GetConsumableName())) {
			manager_player.HealPlayer(consumable_food.GetHPGain());
			RemoveFromInventory(consumable);
		}
		else if (consumable.Equals(consumable_medicine.GetConsumableName())) {
			manager_player.HealPlayer(consumable_medicine.GetHPGain());
			RemoveFromInventory(consumable);
		}
		else {
			Debug.Log("Passed Consumable does not match any prefab names");
		}
	}

	public float GetConsumableWeight(string item_name) {
		switch (item_name) {
			case "Food":
				return consumable_food.GetWeight();
			case "Medicine":
				return consumable_medicine.GetWeight();
			default:
				return 0;
		}
	}

	public float GetMaterialWeight(string item_name) {
		switch (item_name) {
			case "Nails":
				return material_nails.GetWeight();
			case "Wood":
				return material_wood.GetWeight();
			case "Metal":
				return material_metal.GetWeight();
			case "Cloth":
				return material_cloth.GetWeight();
			default:
				return 0;
		}
	}

	public void UseMaterials(string material_name, int count) {
		player_inventory.UseMaterial(material_name, count);
	}

	public float GetPlayerCarryWeight() {
		return manager_player.GetCarryWeight();
	}

	public bool CheckSave() {
		return manager_save.CheckSave();
	}

	public List<int> LoadInventory() {
		return manager_save.LoadInventory();
	}

	// UI Functions
	public void UpdateUIInventory() {
		manager_ui.UpdateInventoryUI();
	}
}
