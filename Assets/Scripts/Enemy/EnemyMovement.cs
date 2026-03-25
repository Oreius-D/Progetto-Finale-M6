using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    // Reference to speed of the enemy. This can be set in the Unity Inspector.
    [SerializeField] private float speed = 2f;

    // Public property for enemy direction, which can be set by other scripts (e.g., EnemyManager).
    public Vector2 Direction { get; set; }

    // Reference to rigidbody and enemy animation, which are initialized in Awake.
    private Rigidbody2D rb;
    private EnemyAnimation enemyAnimation;

    // Reference to the EnemyManager, which is used to access the player's position.
    private EnemyManager enemyManager;
    private Transform playerTransform;

    // Value to control magnitude of the enemy's movement. This is calculated in FixedUpdate based on the direction and speed.
    [SerializeField] private float movementMagnitude = 0.0001f;

    // Enemy damage value, which can be set in the Unity Inspector. This value is used when the enemy collides with the player to determine how much damage to deal.
    [SerializeField] private int damage = 1;

    // Awake is called when the script instance is being loaded. It initializes references to components and the player.
    private void Awake()
    {
        // Get references to components and manager
        rb = GetComponent<Rigidbody2D>();
        enemyAnimation = GetComponentInChildren<EnemyAnimation>();

        enemyManager = GetComponentInParent<EnemyManager>();
        playerTransform = enemyManager ? enemyManager.Player : null;
    }

    // FixedUpdate is called at a fixed interval and is used for physics updates.
    private void FixedUpdate()
    {
        // If there is no player transform, we cannot move towards the player, so we set the direction to zero and return early.
        if (!playerTransform)
        {
            Direction = Vector2.zero;
            return;
        }

        // Calculate the direction towards the player and move the enemy in that direction. The movement is based on the speed and the time since the last physics update (Time.fixedDeltaTime).
        Vector2 enemyPosition = rb.position;
        Vector2 playerPosition = playerTransform.position;

        // Calculate the direction towards the player. If the distance to the player is greater than the movement magnitude, we normalize the direction vector. Otherwise, we set the direction to zero to prevent jittering when close to the player.
        Vector2 toPlayer = playerPosition - enemyPosition;
        Direction = toPlayer.sqrMagnitude > movementMagnitude ? toPlayer.normalized : Vector2.zero;

        // Move the enemy towards the player using Rigidbody2D.MovePosition for smooth movement that interacts well with physics. The new position is calculated by moving from the current enemy position towards the player position at a speed defined by the speed variable.
        Vector2 newPosition = Vector2.MoveTowards(enemyPosition, playerPosition, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    // On Collision with the player, we can implement logic such as dealing damage to the player or triggering an attack animation.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kill or damage the player if collided with the player using interface
        var damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage); // Example damage value, can be adjusted
        }

    }
}
