using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Implements a camera that follows an entity.
//               Do not make this a child of the entity to be followed.
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class FollowCam : MonoBehaviour
{
    [SerializeField] float z_offset = -10.0f;
    public GameObject entity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector3(entity.transform.position.x, entity.transform.position.y, z_offset);
    }
}
