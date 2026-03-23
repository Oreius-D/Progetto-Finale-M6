using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;      // Animator controlling player animations
    [SerializeField] private SpriteRenderer sr;      // SpriteRenderer used to flip the sprite

    private Vector2 lastDir = Vector2.down;           // Last non zero movement direction

    /// <summary>
    /// Updates player animation parameters based on movement direction.
    /// Should be called every frame by the player movement logic.
    /// </summary>
    public void UpdateAnimation(Vector2 direction)
    {
        // Determine whether the player is moving
        bool isMoving = direction.sqrMagnitude > 0.001f;
        animator.SetBool("IsMoving", isMoving);

        // Store last valid direction to preserve facing direction while idle
        if (isMoving)
            lastDir = direction;

        // Flip sprite horizontally for left/right movement
        if (lastDir.x > 0.01f)
            sr.flipX = false;
        else if (lastDir.x < -0.01f)
            sr.flipX = true;

        // If the blend tree only contains the right facing animation (1,0),
        // keep X always positive and mirror using sprite flipping
        float x = Mathf.Abs(lastDir.x);
        float y = lastDir.y;

        // Snap direction to 4 cardinal directions
        // to avoid diagonal jitter and ensure consistent animations
        if (x > Mathf.Abs(y))
        {
            // Horizontal movement
            y = 0f;
            x = 1f; // side
        }
        else
        {
            // Vertical movement
            x = 0f;
            y = Mathf.Sign(y); // up or down
        }

        // Update blend tree parameters
        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}