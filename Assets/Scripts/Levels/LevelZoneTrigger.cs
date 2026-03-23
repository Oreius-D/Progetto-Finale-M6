using UnityEngine;

public class LevelZoneTrigger : MonoBehaviour
{
    private PlayerLevelTrigger level;   // Reference to the player's level controller

    private void Awake()
    {
        // Get the PlayerLevelTrigger component from the parent object
        level = GetComponentInParent<PlayerLevelTrigger>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Try to get the LevelZone data from the collider we entered
        LevelZone zone = other.GetComponent<LevelZone>();
        if (zone == null || level == null) return;

        // Toggle between previous and next level based on current level
        if (level.CurrentLevel == zone.previousLvl)
            level.SetLevel(zone.nextLvl);
        else
            level.SetLevel(zone.previousLvl);
    }
}