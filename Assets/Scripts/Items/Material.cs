using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class for items of the type upgrade material and their properties
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------
public class Material : Item {

	[SerializeField] string material_name;
	[SerializeField] string description;

	public string GetMaterialName() { return material_name; }
	public string GetMaterialDescription() { return description; }
}
