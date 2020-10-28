using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Class determining basic entity behaviour and statistics
//	Contributors:
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class Entity : MonoBehaviour
{
	float move_speed = 1.0f;

	// Start is called before the first frame update
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		
	}

    public void move(float x, float y)
    {
		x = x * Time.deltaTime * move_speed + gameObject.transform.position.x;
		y = y * Time.deltaTime * move_speed + gameObject.transform.position.y;
		gameObject.transform.position = new Vector3(x, y, 0.0f);
    }
}
