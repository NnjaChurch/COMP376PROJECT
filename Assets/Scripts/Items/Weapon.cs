﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Collider2D hit_box;
    ContactFilter2D filter;
    public LayerMask layer_mask;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseWeapon()
    {
        List<Collider2D> collisions = new List<Collider2D>();
        int n = Physics2D.OverlapCollider(hit_box, filter, collisions); //TODO - Fix: filter isnt working, i don't know why.
        foreach (Collider2D collision in collisions)
        {
            if (collision.gameObject.layer == 9)
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(0.0f);
                }
            }
        }
    }
}
