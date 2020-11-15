using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for object looting and random item generation
//	Contributors: Colin
//	Endpoints: 
// ----------------------------------------------------------------------------------------------------


public class LootManager : MonoBehaviour {
	LootTable bag;
	LootTable furniture;
	LootTable vehicle;

	Player player;

	List<string> foundWeapons = new List<string>();
	List<string> foundArmour = new List<string>();

	// Start is called before the first frame update
	void Start() {
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

		player = FindObjectOfType<Player>();

	}

	// Update is called once per frame
	void Update() {
		
	}

	public void LootKeyPressed()
    {
		const float lootRadius = 0.5f;
		// is the player near a lootbag(s)?
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, lootRadius);
		List<Collider2D> lootableColliders = new List<Collider2D>();
		if (lootableColliders != null) {
			foreach (var hitCollider in hitColliders) {
				if (hitCollider.tag == "Lootable") {
					lootableColliders.Add(hitCollider);
				}
			}

			foreach (var lootable in lootableColliders) {
				if (lootable.name.Contains("Lootbag")) {
					LootUIEntity bagLoot = GenerateLootForUI("bag");
					Debug.Log(bagLoot.itemType + " - " + bagLoot.item_name);
				}
			}
		}
		else {
			Debug.Log("No lootable objects in range.");
		}
	}

	// Generates a list of loot
	// Inputs: 
	//	Category (string): bag, furniture, vehicle
	//	(optional) noDropPercentage (float): give a chance of no loot dropped (ie. dropPercentage = 0.5 means there is a 50% chance of loot dropping). Defaults to 1
	// Outputs: An object with the necessary information for the UI to display
	public LootUIEntity GenerateLootForUI(string category, float noDropPercentage = 1.0f)
    {
		float rand = noDropPercentage == 0 ? -1.0f : Random.Range(1, (100 * (1.0f / noDropPercentage)));
		LootUIEntity entity;
		switch (category)
        {
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

	public void WeaponFound(string weaponName)
    {
		foundWeapons.Add(weaponName);
		bag.removeWeaponFromTable(weaponName);
		furniture.removeWeaponFromTable(weaponName);
		vehicle.removeWeaponFromTable(weaponName);
    }
	public void ArmourFound(string armourName)
    {
		foundArmour.Add(armourName);
		bag.removeArmourFromTable(armourName);
		furniture.removeArmourFromTable(armourName);
		vehicle.removeArmourFromTable(armourName);
    }

}

public class LootUIEntity
{
	public System.Type itemType;
	public string item_name;

	public LootUIEntity(System.Type t, string n)
    {
		itemType = t;
		item_name = n;
    }
}

public class LootTable
{
	RangeInt consumableRange;
	RangeInt materialRange;
	RangeInt weaponRange;
	RangeInt armourRange;

	string[] consumables = new string[] { "Food", "Medicine" };
	string[] materials = new string[] { "Nails", "Wood", "Metal", "Cloth" };
	List<string> weapons = new List<string> { "Bat", "Knife", "Sword", "Rake" };
	List<string> armour = new List<string> { "Light", "Medium", "Heavy" };

	public LootTable(RangeInt c, RangeInt m, RangeInt w, RangeInt a)
	{
		consumableRange = c;
		materialRange = m;
		weaponRange = w;
		armourRange = a;
	}

	public void removeWeaponFromTable(string weaponName)
    {
		int weaponIdx = weapons.IndexOf(weaponName);
		weapons[weaponIdx] = "PASS";
    }

	public void removeArmourFromTable(string armourName)
	{
		int armourIdx = armour.IndexOf(armourName);
		armour[armourIdx] = "PASS";
	}

	public LootUIEntity FindLootEntityInRange(float p1)
    {
		// TODO: add variety of options for each item type
		if (consumableRange.start < p1 && consumableRange.end > p1)
        {
			// Determine which consumable will drop
			int rand_idx = Random.Range(0, consumables.Length - 1);
			return new LootUIEntity(typeof(Consumable), consumables[rand_idx]);
        }
		else if (materialRange.start < p1 && materialRange.end > p1)
		{
			// Determine which material will drop
			int rand_idx = Random.Range(0, materials.Length - 1);
			return new LootUIEntity(typeof(Material), materials[rand_idx]);
		}
		else if (weaponRange.start < p1 && weaponRange.end > p1)
		{
			// Determine which weapon will drop			
			int rand_idx = Random.Range(0, weapons.Count - 1);
			string weaponName = weapons[rand_idx];
			if (weaponName == "PASS")
            {
				rand_idx = Random.Range(0, materials.Length - 1);
				return new LootUIEntity(typeof(Material), materials[rand_idx]);
			}
            else
            {
				return new LootUIEntity(typeof(Weapon), weaponName);
			}
		}
		else if (armourRange.start < p1 && armourRange.end > p1)
		{
			// Determine which armour will drop
			int rand_idx = Random.Range(0, armour.Count - 1);
			string armourName = armour[rand_idx];
			if (armourName == "PASS")
			{
				rand_idx = Random.Range(0, materials.Length - 1);
				return new LootUIEntity(typeof(Material), materials[rand_idx]);
			}
			else
			{
				return new LootUIEntity(typeof(Armour), armourName);
			}
		}

		return new LootUIEntity(null, "No loot");
	}
};