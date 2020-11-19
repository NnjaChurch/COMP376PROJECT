using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for all items and the common interactions
//	Contributors: Colin
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Item : MonoBehaviour {

	[SerializeField] float weight;

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public float GetWeight() { return weight; }
}
