using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;      // Animator controlling enemy animations
    [SerializeField] private SpriteRenderer sr;      // SpriteRenderer used to flip the sprite

    private Vector2 lastDir = Vector2.down;           // Last non zero movement direction

    /// <summary>
    /// Updates animation parameters based on movement direction.
    /// Should be called every frame by the enemy logic.
    /// </summary>
    public void UpdateAnimation(Vector2 direction)
    {
        // Check if the enemy is moving
        bool isMoving = direction.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);

        // Store last valid direction to keep facing when idle
        if (isMoving)
            lastDir = direction;

        // Flip sprite horizontally based on movement direction
        if (lastDir.x > 0.01f)
            sr.flipX = false;
        else if (lastDir.x < -0.01f)
            sr.flipX = true;

        // If the blend tree only uses the right facing animation (1,0),
        // we mirror it using sprite flipping
        float x = Mathf.Abs(lastDir.x);
        float y = lastDir.y;

        // Snap direction to 4 cardinal directions for consistency
        if (x > Mathf.Abs(y))
        {
            // Horizontal movement
            y = 0f;
            x = 1f;
        }
        else
        {
            // Vertical movement
            x = 0f;
            y = Mathf.Sign(y);
        }

        // Update blend tree parameters
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}