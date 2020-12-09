using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for object looting and random item generation
//	Contributors: Colin
//	Endpoints: 
// ----------------------------------------------------------------------------------------------------


public class LootManager : MonoBehaviour {

	// Manager References
	[SerializeField] UIManager manager_UI;
	[SerializeField] InventoryManager manager_inventory;
	[SerializeField] PlayerManager player_manager;

	LootTable bag;
	LootTable furniture;
	LootTable vehicle;

	List<string> found_weapons = new List<string>();
	List<string> found_armour = new List<string>();

	public bool Initialize() {
		// loot table ranges for consumables, materials, weapons and armour
		// TODO: tweak these ranges

		//bag
		RangeInt c = new RangeInt(1, 24);
		RangeInt m = new RangeInt(26, 24);
		RangeInt w = new RangeInt(51, 24);
		RangeInt a = new RangeInt(76, 24);
		bag = new LootTable(c, m, w, a);

		//furniture
		c = new RangeInt(1, 24);
		m = new RangeInt(26, 24);
		w = new RangeInt(51, 24);
		a = new RangeInt(76, 24);
		furniture = new LootTable(c, m, w, a);

		//vehicle
		c = new RangeInt(1, 24);
		m = new RangeInt(26, 24);
		w = new RangeInt(51, 24);
		a = new RangeInt(76, 24);
		vehicle = new LootTable(c, m, w, a);

		// Update Found Weapons and Armour
		IDictionary<string, int> found_w = manager_inventory.GetFoundWeapons();
		foreach(KeyValuePair<string, int> weapon in found_w) {
			if(weapon.Value == 1) {
				found_weapons.Add(weapon.Key);
			}
		}
		IDictionary<string, int> found_a = manager_inventory.GetFoundArmour();
		foreach(KeyValuePair<string, int> armour in found_a) {
			if (armour.Value == 1) {
				found_armour.Add(armour.Key);
			}
		}

		return true;
	}

	public void LootKeyPressed() {
		const float lootRadius = 0.5f;
		// is the player near a lootbag(s)?
		Vector3 player_position = player_manager.GetPlayerPosition();
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player_position, lootRadius);
		List<Collider2D> lootableColliders = new List<Collider2D>();
		if (lootableColliders != null) {
			foreach (var hitCollider in hitColliders) {
				if (hitCollider.tag.Contains("Lootable")) {
					lootableColliders.Add(hitCollider);
				}
			}

			foreach (var lootable in lootableColliders) {
				Lootbag lootbag = lootable.GetComponent<Lootbag>();
				if (lootable.tag.Contains("Lootable") && lootbag != null) {
					Debug.Log(lootbag.GetItems()[0].item_name);
					//TODO: OPEN THE LOOT UI - right now this is scanning every bag in the vicinity. Maybe change to open only the closest?
					manager_UI.DisplayLoot(lootbag);
				}
			}
		}
		else {
			Debug.Log("No lootable objects in range.");
		}
	}

	// Generates a list of loot
	// Inputs: 
	// Category (string): bag, furniture, vehicle
	// (optional) percentDropRate (float): give a chance of no loot dropped (ie. percentDropRate = 0.5 means there is a 50% chance of loot dropping). Defaults to 1
	// Outputs: An object with the necessary information for the UI to display
	public LootUIEntity GenerateLootForUI(string category, float percentDropRate = 1.0f) {
		float rand = percentDropRate == 0 ? -1.0f : Random.Range(1, (100 * (1.0f / percentDropRate)));
		LootUIEntity entity;
		switch (category) {
			case "bag":
				entity = bag.FindLootEntityInRange(rand);
				break;
			case "furniture":
				entity = furniture.FindLootEntityInRange(rand);
				break;
			case "vehicle":
				entity = vehicle.FindLootEntityInRange(rand);
				break;
			default:
				entity = bag.FindLootEntityInRange(rand);
				break;
		}
		return entity;
	}

	public void WeaponFound(string weaponName) {
		found_weapons.Add(weaponName);
		bag.removeWeaponFromTable(weaponName);
		furniture.removeWeaponFromTable(weaponName);
		vehicle.removeWeaponFromTable(weaponName);

		SanitizeExistingLootbags(weaponName);
	}
	public void ArmourFound(string armourName) {
		found_armour.Add(armourName);
		bag.removeArmourFromTable(armourName);
		furniture.removeArmourFromTable(armourName);
		vehicle.removeArmourFromTable(armourName);

		SanitizeExistingLootbags(armourName);
	}

	// Scans for and removes any item_name from existing lootbags, and replaces with materials
	public void SanitizeExistingLootbags(string item_name) {
		Lootbag[] existing_lootbags = GameObject.FindObjectsOfType<Lootbag>();
		foreach (Lootbag lootbag in existing_lootbags) {
			List<LootUIEntity> entities = lootbag.GetItems();
			int index = entities.FindIndex(e => e.item_name == item_name);
			if (index > -1) { lootbag.ResetItems(); }
		}
	}

}

public class LootUIEntity {
	public System.Type itemType;
	public string item_name;

	public LootUIEntity(System.Type t, string n) {
		itemType = t;
		item_name = n;
	}
}

public class LootTable {
	RangeInt consumableRange;
	RangeInt materialRange;
	RangeInt weaponRange;
	RangeInt armourRange;

	string[] consumables = new string[] { "Food", "Medicine" };
	string[] materials = new string[] { "Nails", "Wood", "Metal", "Cloth" };
	List<string> weapons = new List<string> { "Bat", "Knife", "Shovel", "Rake" };
	List<string> armour = new List<string> { "Light Armour", "Medium Armour", "Heavy Armour" };

	public LootTable(RangeInt c, RangeInt m, RangeInt w, RangeInt a) {
		consumableRange = c;
		materialRange = m;
		weaponRange = w;
		armourRange = a;
	}

	public void removeWeaponFromTable(string weaponName) {
		int weaponIdx = weapons.IndexOf(weaponName);
		weapons[weaponIdx] = "PASS";
	}

	public void removeArmourFromTable(string armourName) {
		int armourIdx = armour.IndexOf(armourName);
		armour[armourIdx] = "PASS";
	}

	public LootUIEntity FindLootEntityInRange(float p1) {
		// TODO: add variety of options for each item type
		if (consumableRange.start < p1 && consumableRange.end > p1) {
			// Determine which consumable will drop
			int rand_idx = Random.Range(0, consumables.Length - 1);
			return new LootUIEntity(typeof(Consumable), consumables[rand_idx]);
		}
		else if (materialRange.start < p1 && materialRange.end > p1) {
			// Determine which material will drop
			int rand_idx = Random.Range(0, materials.Length - 1);
			return new LootUIEntity(typeof(Material), materials[rand_idx]);
		}
		else if (weaponRange.start < p1 && weaponRange.end > p1) {
			// Determine which weapon will drop			
			int rand_idx = Random.Range(0, weapons.Count - 1);
			string weaponName = weapons[rand_idx];
			if (weaponName == "PASS") {
				rand_idx = Random.Range(0, materials.Length - 1);
				return new LootUIEntity(typeof(Material), materials[rand_idx]);
			}
			else {
				return new LootUIEntity(typeof(Weapon), weaponName);
			}
		}
		else if (armourRange.start < p1 && armourRange.end > p1) {
			// Determine which armour will drop
			int rand_idx = Random.Range(0, armour.Count - 1);
			string armourName = armour[rand_idx];
			if (armourName == "PASS") {
				rand_idx = Random.Range(0, materials.Length - 1);
				return new LootUIEntity(typeof(Material), materials[rand_idx]);
			}
			else {
				return new LootUIEntity(typeof(Armour), armourName);
			}
		}

		return new LootUIEntity(null, "No loot");
	}
};