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
	[SerializeField] string weapon_name;
	[SerializeField] int BASE_DAMAGE;
	[SerializeField] float speed_modifier;
	[SerializeField] int DAMAGE_SCALING;

	int upgrade_tier;
	int damage;


	// Start is called before the first frame update
	void Start() {
		filter.SetLayerMask(layer_mask);
		upgrade_tier = 0;
		CalculateDamage();
	}

	public void UpgradeWeapon() {
		upgrade_tier++;
		CalculateDamage();
	}

	public void UseWeapon(int base_damage) {
		List<Collider2D> collisions = new List<Collider2D>();
		int n = Physics2D.OverlapCollider(hit_box, filter, collisions);
		foreach (Collider2D collision in collisions) {
			EnemyStats enemyStats = collision.gameObject.GetComponent<EnemyStats>();
			PlayerStats playerStats = null;
			Transform playerStatsTransform = collision.gameObject.transform.Find("PlayerStats");
			if (playerStatsTransform != null)
            {
				playerStats = playerStatsTransform.GetComponent<PlayerStats>();
			}
			if (enemyStats != null) {
				enemyStats.TakeDamage(base_damage + damage);
			}
			if (playerStats != null)
			{
				playerStats.TakeDamage(base_damage + damage);
			}
		}
	}

	public float GetSpeedModifier() {
		return speed_modifier;
	}

	public int GetDamage() {
		return damage;
	}

	public string GetWeaponName() {
		return weapon_name;
	}

	public int GetWeaponTier() {
		return upgrade_tier;
	}

	public void SetWeaponTier(int tier) {
		upgrade_tier = tier;
		CalculateDamage();
	}

	private void CalculateDamage() {
		damage = BASE_DAMAGE + (upgrade_tier * DAMAGE_SCALING);
	}
}
