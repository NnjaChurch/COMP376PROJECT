using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for the inventory used by Player
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Inventory : MonoBehaviour
{
	float current_weight;
	bool isEncumbered;

	public IDictionary<string, int> consumables = new Dictionary<string, int>();
	public IDictionary<string, int> materials = new Dictionary<string, int>();
	public IDictionary<string, int> weapons = new Dictionary<string, int>();
	public IDictionary<string, int> armours = new Dictionary<string, int>();

	// Start is called before the first frame update
	void Start()
	{
		current_weight = 0;
		isEncumbered = false;

		consumables.Add("Food", 1);
		consumables.Add("Medicine", 0);

		materials.Add("Nails", 0);
		materials.Add("Wood", 0);
		materials.Add("Metal", 0);
		materials.Add("Cloth", 2);

		weapons.Add("Knives", 0);
		weapons.Add("Bats", 1);
		weapons.Add("Swords", 0);
		weapons.Add("Rakes", 0);

		armours.Add("Light", 0);
		armours.Add("Medium", 0);
		armours.Add("Heavy", 1);

		InventoryUI.inventory_updated = true;
	}

	// Update is called once per frame
	void Update()	{

	}

	void UpdateWeight(bool add, float itemWeight)
	{
		current_weight = add ? current_weight + itemWeight : current_weight - itemWeight;
		Player player = gameObject.GetComponentInParent<Player>();
		float carry_capacity = 0; //player.GetCarryWeight();

		if (current_weight > carry_capacity)
		{
			isEncumbered = true;
		}
		else {
			isEncumbered = false;
		}
	}
	public void AddToInventory(Item i)
	{
		int currentCount;
		switch (i)
		{
			case Consumable c:
				currentCount = consumables[c.GetConsumableName()];
				consumables[c.GetConsumableName()] = currentCount + 1;
				break;
			case Material m:
				currentCount = materials[m.GetMaterialName()];
				materials[m.GetMaterialName()] = currentCount + 1;
				break;
			case Weapon w:
				currentCount = weapons[w.GetWeaponName()];
				weapons[w.GetWeaponName()] = currentCount + 1;
				break;
			case Armour a:
				currentCount = armours[a.GetArmourName()];
				armours[a.GetArmourName()] = currentCount + 1;
				break;
		}

		InventoryUI.inventory_updated = true; // So that inventory UI updates during next Update()
		UpdateWeight(true, i.GetWeight());
	}
	public void RemoveFromInventory(Item i)
	{
		int currentCount;
		switch (i)
		{
			case Consumable c:
				currentCount = consumables[c.GetConsumableName()];
				consumables[c.GetConsumableName()] = currentCount - 1;
				break;
			case Material m:
				currentCount = materials[m.GetMaterialName()];
				materials[m.GetMaterialName()] = currentCount - 1;
				break;
			case Weapon w:
				currentCount = weapons[w.GetWeaponName()];
				weapons[w.GetWeaponName()] = currentCount - 1;
				break;
			case Armour a:
				currentCount = armours[a.GetArmourName()];
				armours[a.GetArmourName()] = currentCount - 1;
				break;
		}

		InventoryUI.inventory_updated = true; // So that inventory UI updates during next Update()
		UpdateWeight(false, i.GetWeight());
	}

	public float GetInventoryWeight() { return current_weight; }
	
}
