using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxLife = 10;        // Maximum life value
    [SerializeField] private bool destroyOnDeath = true; // If true, the GameObject is destroyed after death animation
    [SerializeField] private AudioClip enemyDeathSoundClip; // Sound played when the entity dies

    private int currentLife;      // Current life value
    private bool isDead;          // Flag to prevent multiple death triggers
    private Animator animator;    // Animator used to play the death animation

    private void Awake()
    {
        // Initialize life and get the Animator from children
        currentLife = maxLife;
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Applies damage to the entity.
    /// If life reaches zero, triggers death logic.
    /// </summary>
    public void TakeDamage(int amount)
    {
        // Ignore damage if already dead
        if (currentLife <= 0) return;

        currentLife -= amount;

        if (currentLife <= 0)
        {
            currentLife = 0;
            Die();
        }
    }

    /// <summary>
    /// Heals the entity without exceeding max life.
    /// Healing is ignored if the entity is dead.
    /// </summary>
    public void Heal(int amount)
    {
        if (currentLife <= 0) return;

        currentLife += amount;
        if (currentLife > maxLife)
            currentLife = maxLife;
    }

    /// <summary>
    /// Handles death logic:
    /// stops AI, disables collisions, plays animation and sound.
    /// </summary>
    private void Die()
    {
        // Prevent multiple executions
        if (isDead) return;
        isDead = true;

        // Disable enemy AI logic
        var enemy = GetComponent<EnemyMovement>();
        if (enemy) enemy.enabled = false;

        // Disable collider to avoid further interactions
        var col = GetComponent<Collider2D>();
        if (col) col.enabled = false;

        // Trigger death animation
        if (animator) animator.SetBool("IsDead", true);

        // Play death sound effect. ToDo: Upgrade to pool + singleton SoundFXManager
        //if (enemyDeathSoundClip && SoundFXManager.Instance != null)
         //   SoundFXManager.Instance.PlaySoundFXClip(enemyDeathSoundClip, transform, 1f);
    }

    /// <summary>
    /// Called via Animation Event at the last frame of the death animation.
    /// </summary>
    public void OnDeathAnimFinished()
    {
        if (destroyOnDeath)
            Destroy(gameObject);
    }

    // Public getters
    public int GetCurrentLife() => currentLife;
    public int GetMaxLife() => maxLife;
    public bool IsDead() => isDead;
}