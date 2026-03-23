using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLevelTrigger : MonoBehaviour
{
    [SerializeField] private SortingGroup sortingGroup; // SortingGroup used to control render order

    [SerializeField] private int orderL0 = 7;   // Sorting order for level 0
    [SerializeField] private int orderL1 = 14;  // Sorting order for level 1
    [SerializeField] private int orderL2 = 20;  // Sorting order for level 2

    // Public read only access to the current level
    public int CurrentLevel => currentLevel;

    private int currentLevel = 0;               // Current logical/vertical level

    private void Awake()
    {
        // Automatically get SortingGroup if not assigned
        if (sortingGroup == null)
            sortingGroup = GetComponent<SortingGroup>();

        // Apply initial sorting order
        ApplySorting();
    }

    /// <summary>
    /// Sets the current level and updates the rendering order accordingly.
    /// </summary>
    public void SetLevel(int level)
    {
        // Avoid unnecessary updates
        if (currentLevel == level) return;

        currentLevel = level;
        ApplySorting();
    }

    /// <summary>
    /// Applies the correct sorting order based on the current level.
    /// </summary>
    private void ApplySorting()
    {
        int order =
            currentLevel == 0 ? orderL0 :
            currentLevel == 1 ? orderL1 :
            orderL2;

        sortingGroup.sortingOrder = order;
    }
}