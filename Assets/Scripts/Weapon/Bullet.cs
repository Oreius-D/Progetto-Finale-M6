using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Lifetime of the bullet in seconds, after which it will be automatically destroyed. This can be set in the Unity Inspector.
    [SerializeField] private float lifetime = 1f;
    // Damage that the bullet will inflict on enemies it hits. This can be set in the Unity Inspector.
    [SerializeField] private int damage = 1;

    // Private reference to the Rigidbody2D component, which is used to control the bullet's movement. This will be assigned in the Awake method.
    private Rigidbody2D rb;

    private void Awake()
    {
        // Get the Rigidbody2D component attached to this GameObject and store it in the rb variable for later use in controlling the bullet's movement.
        rb = GetComponent<Rigidbody2D>();
        // Schedule the destruction of this bullet GameObject after the specified lifetime to prevent it from existing indefinitely in the scene.
        Destroy(gameObject, lifetime);
    }

    // This method initializes the bullet's movement and rotation based on the given direction and speed. It is called when the bullet is spawned.
    public void Init(Vector2 direction, float bulletSpeed)
    {
        // Normalize the direction vector to ensure consistent movement speed regardless of the original magnitude of the direction vector.
        Vector2 dir = direction.normalized;

        // Calculate the angle in degrees from the direction vector using the arctangent function, and convert it from radians to degrees.
        // This angle will be used to rotate the bullet so that it faces the direction it is moving.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Rotate the bullet to face the direction of movement. The -90f adjustment is often needed because the default orientation of the sprite may not align with the direction vector.
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // Set the velocity of the bullet's Rigidbody2D to move it in the specified direction at the given speed.
        rb.velocity = dir * bulletSpeed;
    }

    // This method allows you to set the damage of the bullet after it has been spawned. Useful since damage is determined by the weapon.
    public void SetDamage(int newDamage) => damage = newDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object we collided with has an IDamageable component, which indicates it can take damage (like an enemy).
        var dmg = collision.GetComponent<IDamageable>();

        // If the collided object can take damage, call its TakeDamage method with the bullet's damage value, and then destroy the bullet GameObject to prevent it from hitting multiple targets.
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Todo: Add logic to handle collisions with other objects, such as walls or the player.
    }
}
