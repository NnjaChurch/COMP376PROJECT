using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining basic entity behaviour and statistics
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Entity : MonoBehaviour
{

	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		
	}

	public int TakeDamage(float damage)
    {
		print("entity is taking damage!");
		return 0;
    }
}
