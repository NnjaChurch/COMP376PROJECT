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

	int upgrade_nails;
	int upgrade_wood;


	void Start() {
		filter.SetLayerMask(layer_mask);
		upgrade_tier = 0;
		CalculateDamage();
		CalculateUpgradeMaterials();
		if (gameObject.transform.parent.tag != "Enemy")
        {
			gameObject.SetActive(false);
		}
	}

	public bool UpgradeWeapon() {
		upgrade_tier++;
		CalculateDamage();
		CalculateUpgradeMaterials();
		return true;
	}

	public void UseWeapon(int base_damage, Stats userStats) {
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
				if (enemyStats.TakeDamage(base_damage + damage) <= 0)
                { // enemy has died, grant weapon user some exp.
					if (typeof(PlayerStats).IsInstanceOfType(userStats))
                    {
						PlayerStats userPlayerStats = (PlayerStats)userStats;
						userPlayerStats.CollectExperience(enemyStats.GetExpReward());
                    }
                }
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

	public List<int> GetWeaponInfo() {
		// Returns list with weapon info { upgrade_tier, damage, damage_scaling }
		return new List<int> { upgrade_tier, damage, DAMAGE_SCALING };
	}

	public List<int> GetMaterialCost() {
		List<int> material_list = new List<int> { upgrade_nails, upgrade_wood, 0, 0 };
		return material_list;
	}

	public void SetWeaponTier(int tier) {
		upgrade_tier = tier;
		CalculateDamage();
		CalculateUpgradeMaterials();
	}

	private void CalculateDamage() {
		damage = BASE_DAMAGE + (upgrade_tier * DAMAGE_SCALING);
	}

	private void CalculateUpgradeMaterials() {
		upgrade_nails = 5 + (5 * upgrade_tier);
		upgrade_wood = 5 + (5 * upgrade_tier);
	}
}
