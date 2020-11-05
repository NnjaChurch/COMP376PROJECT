using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for items of the type weapon, their properties and interactions
//	Contributors: Jordan, Kevin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Weapon : Item {
	// Collision Variables
	public Collider2D hit_box;
	ContactFilter2D filter;
	public LayerMask layer_mask;

	// Attributes
	string weapon_name;
	int upgrade_tier;
	float speed_modifier;   // Percentage
	[SerializeField] int damage;             // Value
	float reach;            // Units
	float arc;              // Degrees


	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {

	}

	public void UseWeapon(int base_damage) {
		List<Collider2D> collisions = new List<Collider2D>();
		int n = Physics2D.OverlapCollider(hit_box, filter, collisions); //TODO - Fix: filter isnt working, i don't know why.
		foreach (Collider2D collision in collisions) {
			if (collision.gameObject.layer == 9) {
				Enemy enemy = collision.gameObject.GetComponent<Enemy>();
				if (enemy != null) {
					if (enemy.GetStats().TakeDamage(base_damage + damage) <= 0)
                    {
						enemy.Kill();
                    }
				}
			}
		}
	}

	public float GetSpeedModifier() {
		return speed_modifier;
	}

	public int GetDamage() {
		return damage;
	}

	public float GetReach() {
		return reach;
	}

	public float GetArc() {
		return arc;
	}

	public string GetWeaponName() { return weapon_name; }
}
