using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour {
	// Start is called before the first frame update
	LootManager loot_manager;

	// bag, vehicle, furnite
	[SerializeField] string item_type;

	List<LootUIEntity> items = new List<LootUIEntity>();

	void Start() {
		loot_manager = GameObject.Find("LootManager").GetComponent<LootManager>();
		LootUIEntity item = loot_manager.GenerateLootForUI(item_type);
		items.Add(item);
	}

	public List<LootUIEntity> GetItems() {
		return items;
	}

	public void ResetItems() {
		items.Clear();
		LootUIEntity item = loot_manager.GenerateLootForUI(item_type);
		items.Add(item);
	}

	public void DestroyLootbag()
    {
		string container_type = this.GetParentType();
		if (container_type == "bag")
		{
			Destroy(transform.gameObject);
		}
		else
		{
			transform.gameObject.tag = "Untagged";
		}
    }

	public string GetParentType()
    {
		switch (transform.gameObject.tag)
        {
			case "Lootable_dresser":
				return "furniture";
			case "Lootable_vehicle":
				return "vehicle";
			default:
				return "bag";
        }
    }
}
