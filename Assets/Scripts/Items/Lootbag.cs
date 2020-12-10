using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbag : MonoBehaviour {
	// Start is called before the first frame update
	LootManager loot_manager;

	// bag, vehicle, furnite
	[SerializeField] string item_type;
	UnityEngine.Material original_material;

	bool glow;
	float minThickness = 0.001f;
	float maxThickness = 0.02f;
	float currentThickness = 0.005f;
	float glowSpeed = 0.04f;
	bool glowUp = true;

	List<LootUIEntity> items = new List<LootUIEntity>();

	void Start() {
		loot_manager = GameObject.Find("LootManager").GetComponent<LootManager>();
		LootUIEntity item = loot_manager.GenerateLootForUI(item_type);
		items.Add(item);

		glow = false;
		HighlightObject();

	}

    private void Update()
    {
        if (glow)
        {
			if (glowUp)
            {
				currentThickness += Time.deltaTime * glowSpeed;
				if (currentThickness >= maxThickness) { glowUp = false; }
            } else
            {
				currentThickness -= Time.deltaTime * glowSpeed;
				if (currentThickness <= minThickness) { glowUp = true; }
            }
			gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_Thickness", currentThickness);
        }
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
		transform.gameObject.GetComponent<SpriteRenderer>().material = original_material;
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

	void HighlightObject()
    {
		original_material = transform.gameObject.GetComponent<SpriteRenderer>().material;
		string container_type = this.GetParentType();
		if (container_type == "furniture")
        {
			transform.gameObject.GetComponent<SpriteRenderer>().material = Resources.Load<UnityEngine.Material>("Dresser_material");
		} else if (container_type == "vehicle")
        {
			transform.gameObject.GetComponent<SpriteRenderer>().material = Resources.Load<UnityEngine.Material>("Vehicle_material");
		}
		glow = true;
    }
}
