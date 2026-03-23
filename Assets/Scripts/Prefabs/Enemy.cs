using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;                 // Enemy movement speed
    [SerializeField] private PlayerController player;     // Reference to the player

    public Vector2 Direction { get; private set; }        // Current movement direction (normalized)

    private Rigidbody2D rb;                               // Rigidbody used for movement
    private EnemyAnimation enemyAnimation;                // Handles enemy animations

    private void Awake()
    {
        // Find the player in the scene
        player = FindObjectOfType<PlayerController>();

        // Cache Rigidbody2D reference
        rb = GetComponent<Rigidbody2D>();

        // Cache EnemyAnimation if it exists on the same GameObject
        if (enemyAnimation == null)
            enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void Update()
    {
        // If player reference is missing stop moving and keep animation in idle state
        if (player == null)
        {
            Direction = Vector2.zero;

            // Stop current movement
            if (rb != null) rb.velocity = Vector2.zero;

            // Update animation to idle
            if (enemyAnimation != null)
                enemyAnimation.UpdateAnimation(Direction);

            return;
        }

        // Move towards the player
        EnemyMovement();

        // Update animation after computing direction
        if (enemyAnimation != null)
            enemyAnimation.UpdateAnimation(Direction);
    }

    /// <summary>
    /// Moves the enemy towards the player using MovePosition / MoveTowards.
    /// Also computes Direction for animation purposes.
    /// </summary>
    private void EnemyMovement()
    {
        // Get current enemy position
        Vector2 enemyPos = rb != null ? rb.position : (Vector2)transform.position;
        Vector2 playerPos = player.transform.position;

        // Compute direction towards the player
        Vector2 toPlayer = (playerPos - enemyPos);
        Direction = toPlayer.sqrMagnitude > 0.0001f ? toPlayer.normalized : Vector2.zero;

        // Compute the next position towards the player
        Vector2 newPos = Vector2.MoveTowards(enemyPos, playerPos, speed * Time.deltaTime);

        // Apply movement using Rigidbody2D if available, otherwise fallback to Transform
        if (rb != null) rb.MovePosition(newPos);
        else transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the enemy collides with the player, trigger instant death (contact = death)
        PlayerController p = collision.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            // Trigger player death logic (animation + cleanup)
            p.Die();

            // Clear the player reference so the enemy stops chasing
            player = null;

            // Stop movement immediately
            Direction = Vector2.zero;
            if (rb != null) rb.velocity = Vector2.zero;

            // Update animation to idle
            if (enemyAnimation != null)
                enemyAnimation.UpdateAnimation(Direction);
        }
    }
}