using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        if (player == null)
        {
            print("Error: Player not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            player.move(0.0f, 1.0f);
        }
        if (Input.GetKey("a"))
        {
            player.move(-1.0f, 0.0f);
        }
        if (Input.GetKey("s"))
        {
            player.move(0.0f, -1.0f);
        }
        if (Input.GetKey("d"))
        {
            player.move(1.0f, 0.0f);
        }
    }
}
