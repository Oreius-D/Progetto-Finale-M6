using System;
using UnityEngine;

// This component can be used for both player and enemies. It handles health, damage, healing, and death logic.
public class Health : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private int maxHealth = 10; // Max health value
    [SerializeField] private bool destroyOnDeath = true; // Whether to destroy the GameObject when it dies

    [Header("Death")]
    [SerializeField] private Animator animator; // Animator to trigger death animation (optional)
    [SerializeField] private string deathBool = "IsDead"; // Name of the bool parameter in the Animator to trigger death animation

    // Current health value, max health, and death state
    public int CurrentHealth { get; private set; }

    // Expose max health and death state as read-only properties
    public int MaxHealth => maxHealth;

    // Indicates whether the object is dead (health is 0)
    public bool IsDead { get; private set; }

    public event Action Changed;   // health changed (damage or heal)
    public event Action Died;      // death event (only triggered once when health reaches 0)

    private Collider2D col; // Cached collider reference

    // Initialize health and cache components
    private void Awake()
    {
        // Cache Animator if not assigned
        if (!animator) animator = GetComponentInChildren<Animator>();
        // Cache Collider2D reference
        col = GetComponent<Collider2D>();
        // Initialize health to max at the start
        CurrentHealth = maxHealth;
        // Invoke the Changed event to notify listeners that health has changed
        Changed?.Invoke();
    }

    // Method to apply damage to the object
    public void TakeDamage(int amount)
    {
        // If already dead, ignore damage
        if (IsDead) return;

        // Reduce current health by the damage amount, ensuring it doesn't go below 0
        CurrentHealth = Mathf.Max(0, CurrentHealth - amount);

        // Invoke the Changed event to notify listeners that health has changed
        Changed?.Invoke();

        // If health has reached 0, trigger death logic
        if (CurrentHealth == 0)
            Die();
    }

    // Method to heal the object
    public void Heal(int amount)
    {
        // If already dead, ignore healing
        if (IsDead) return;

        // Increase current health by the healing amount, ensuring it doesn't exceed max health
        CurrentHealth = Mathf.Min(maxHealth, CurrentHealth + amount);
        // Invoke the Changed event to notify listeners that health has changed
        Changed?.Invoke();
    }

    // Method to handle death logic
    private void Die()
    {
        // If already dead, do nothing (prevents multiple death triggers)
        if (IsDead) return;
        // Set death state to true
        IsDead = true;
        // Disable the collider to prevent further interactions (e.g., damage, collisions) after death
        if (col) col.enabled = false;
        // Trigger death animation by setting the specified bool parameter in the Animator
        if (animator) animator.SetBool(deathBool, true);

        // Invoke the Died event to notify listeners that the object has died
        Died?.Invoke();
    }

    // This method can be called as an animation event at the end of the death animation to destroy the GameObject if destroyOnDeath is true
    public void OnDeathAnimFinished()
    {
        if (destroyOnDeath)
            Destroy(gameObject);

        //Todo: Consider implementing respawn logic here instead of destroying the object
    }
}
