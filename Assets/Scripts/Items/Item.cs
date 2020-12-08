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

	public float GetWeight() { return weight; }
}
