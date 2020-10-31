using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining player specific behaviour and statistics
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Player : Entity
{
	public Camera player_cam;
	public Weapon weapon;
	// Start is called before the first frame update
	void Start() 
	{
	}

	// Update is called once per frame
	void Update() 
	{

		Vector2 direction = player_cam.ScreenToWorldPoint(Input.mousePosition);
		direction = (direction - (Vector2)gameObject.transform.position).normalized;
		gameObject.transform.up = direction;

	}

	public Weapon GetWeapon()
    {
		return weapon;
    }


}
