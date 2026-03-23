using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;     // Player movement speed
    [SerializeField] private Animator animator;        // Animator for death animation

    public Vector2 Direction { get; private set; }    // Normalized movement direction

    private float horizontal;                         // Horizontal input value
    private float vertical;                           // Vertical input value
    private Rigidbody2D rb;                           // Rigidbody used for movement
    private PlayerAnimation playerAnimation;           // Handles player animation
    private bool dead;                                 // Prevents multiple death triggers

    private void Awake()
    {
        // Automatically get Animator if not assigned in Inspector
        if (!animator)
            animator = GetComponentInChildren<Animator>();
    }

    // Called before the first frame update
    void Start()
    {
        // Cache required components
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    // Called once per frame (input and animation)
    void Update()
    {
        // Read raw input axes
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Compute normalized movement direction
        Direction = new Vector2(horizontal, vertical).normalized;

        // Update animation parameters
        playerAnimation.UpdateAnimation(Direction);
    }

    // Called at fixed time intervals (physics movement)
    void FixedUpdate()
    {
        // Move the player using Rigidbody2D
        Vector2 newPosition = rb.position + (Direction * speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    /// <summary>
    /// Triggers player death:
    /// stops movement and physics, disables collisions,
    /// starts death animation and fades out background music.
    /// </summary>
    public void Die()
    {
        // Prevent multiple executions
        if (dead) return;
        dead = true;

        // Stop physics and movement
        var rb = GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false; // Freeze physics simulation
        }

        // Disable collider to avoid repeated collisions
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        // Trigger death animation
        if (animator)
            animator.SetBool("IsDead", true);

        // Fade out background music
        SoundFXManager.Instance.FadeOutMusic(1.0f);
    }

    /// <summary>
    /// Called via Animation Event on the last frame of the death animation.
    /// </summary>
    public void OnDeathAnimFinished()
    {
        Destroy(gameObject); // Or trigger Game Over logic
    }
}