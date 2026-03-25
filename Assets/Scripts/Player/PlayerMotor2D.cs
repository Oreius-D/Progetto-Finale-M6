using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class responsible for handling player movement in 2D space, applying physics forces to the Rigidbody2D component based on input from the PlayerInput class.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor2D : MonoBehaviour
{
    //Reference to speed, set in the inspector
    [SerializeField] private float moveSpeed = 5f;
    // Player input reference
    [SerializeField] private PlayerInput playerInput;

    // 2D Direction
    public Vector2 moveDirection { get; private set; }

    // Reference to the Rigidbody2D component
    private Rigidbody2D rb;

    //Awake is called when the script instance is being loaded
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(!playerInput) playerInput = GetComponent<PlayerInput>();
    }

    //Update is called once per frame
    private void Update()
    {
        // Get movement input from the PlayerInput component
        moveDirection = playerInput ? playerInput.movementInput : Vector2.zero;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate, used here for physics calculations
    private void FixedUpdate()
    {
        // Apply movement to the Rigidbody2D based on the moveDirection and moveSpeed
        Vector2 newPosition = rb.position + (moveDirection * moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    public void SetSpeed(float newSpeed) => moveSpeed = newSpeed;
}
