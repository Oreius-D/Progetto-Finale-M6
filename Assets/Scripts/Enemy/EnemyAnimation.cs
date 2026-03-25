using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    // Reference to the Animator component, Sprite Renderer and enemy set in the inspector
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private EnemyMovement enemy;

    // Threshold to determine if the player is considered moving
    [SerializeField] private float movementThreshold = 0.001f;
    // Threshold to determine if the player is facing left or right for flipping the sprite
    [SerializeField] private float flipThreshold = 0.1f;
    // Zero and One thresholds for determining the dominant direction for animations
    [SerializeField] private float zeroThreshold = 0f;
    [SerializeField] private float oneThreshold = 1f;

    // Last Direction the player was facing, used for idle animations
    private Vector2 lastDirection = Vector2.down;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if (!animator) animator = GetComponentInChildren<Animator>();
        if (!spriteRenderer) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (!enemy) enemy = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the current movement direction from the Enemy component
        Vector2 movementDirection = enemy ? enemy.Direction : Vector2.zero;
        // Update the last direction if the enemy is moving
        UpdateAnimation(movementDirection);

    }

    public void UpdateAnimation(Vector2 movementDirection)
    {
        // If the player is moving, update the last direction and set the animator parameters
        var isMoving = movementDirection.sqrMagnitude > movementThreshold;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
            lastDirection = movementDirection;

        if (lastDirection.x > flipThreshold) spriteRenderer.flipX = false;
        else if (lastDirection.x < -flipThreshold) spriteRenderer.flipX = true;

        float x = Mathf.Abs(lastDirection.x);
        float y = lastDirection.y;

        // Set x and y parameters for the animator to control directional animations
        if (x > Mathf.Abs(y))
        {
            y = zeroThreshold;
            x = oneThreshold;
        }
        else
        {
            x = zeroThreshold;
            y = Mathf.Sign(y);
        }

        animator.SetFloat("X", x);
        animator.SetFloat("Y", y);
    }
}