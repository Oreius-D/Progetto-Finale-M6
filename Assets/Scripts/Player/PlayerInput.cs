using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for handling player input.
public class PlayerInput : MonoBehaviour
{
    //Movement input 
    public Vector2 movementInput { get; private set; } // Stores the current movement input as a Vector2

    // Actions
    public bool dashPressed { get; private set; } // Stores whether the interact button is pressed
    public bool interactPressed { get; private set; } // Stores whether the interact button is pressed
    public bool reloadPressed { get; private set; } // Stores whether the attack button is pressed

    // Update is called once per frame
    private void Update()
    {
        // Read movement input from the horizontal and vertical axes
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        // Todo : Add other inputs here (dash, interact, reload, etc.) in future updates
    }

    // Late Update is called after all Update functions have been called, used here to reset action inputs
    private void LateUpdate()
    {
        // Todo : Reset action inputs here (dashPressed, interactPressed, reloadPressed) after they have been processed by the PlayerController in the same frame
    }
}
