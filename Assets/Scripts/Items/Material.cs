using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for items of the type upgrade material and their properties
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Material : Item {
	public string material_name;
	string description;
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public string GetMaterialName() { return material_name; }
}
