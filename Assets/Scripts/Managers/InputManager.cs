﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------------------------------
//	Description: Manages player inputs.
//	Contributors: Jordan
//	Endpoints:
// ----------------------------------------------------------------------------------------------------

public class InputManager : MonoBehaviour
{
    public Player player;
    public Movement player_movement;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player_movement.SetMovement(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player_movement.SetSprint(Input.GetKey("space"));
    }

    private void FixedUpdate()
    {
    }
}