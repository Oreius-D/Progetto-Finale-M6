using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifetime = 1f;   // Time before the bullet is automatically destroyed
    [SerializeField] int damage = 1;        // Damage dealt on hit

    [SerializeField] private AudioClip shootSoundClip; // Sound played when the bullet is fired

    public float Speed => _speed;            // Read only access to bullet speed

    private Rigidbody2D rb;                 // Rigidbody used for movement
    private float _speed;                   // Bullet speed
    private Vector2 _dir;                   // Normalized movement direction

    private void Awake()
    {
        // Cache Rigidbody2D reference
        rb = GetComponent<Rigidbody2D>();

        // Destroy the bullet after a fixed lifetime, even if it hits nothing
        Destroy(gameObject, lifetime);
    }

    /// <summary>
    /// Initializes the bullet with direction and speed.
    /// Called immediately after instantiation.
    /// </summary>
    public void Init(Vector2 direction, float speed)
    {
        // Play shooting sound effect
        SoundFXManager.Instance.PlaySoundFXClip(shootSoundClip, transform, 1f);

        // Store normalized direction and speed
        _dir = direction.normalized;
        _speed = speed;

        // Rotate the bullet to face its movement direction
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        // -90 because the bullet sprite is oriented upward by default

        // Apply constant velocity
        rb.velocity = _dir * _speed;
    }

    /// <summary>
    /// Updates the bullet damage value.
    /// </summary>
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check collision using Layer (not Tag) for better performance
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // Try to get the LifeController from the enemy
            LifeController life = collision.GetComponent<LifeController>();
            if (life != null)
            {
                // Apply damage to the enemy
                life.TakeDamage(damage);
            }

            // Destroy the bullet on impact
            Destroy(gameObject);
        }
    }
}