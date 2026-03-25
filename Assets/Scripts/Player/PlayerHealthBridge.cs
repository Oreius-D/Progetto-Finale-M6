using UnityEngine;

// This component bridges the Health component of the player with the GameEvents system to notify other parts of the game about health changes and death events.
public class PlayerHealthBridge : MonoBehaviour
{
    [SerializeField] private Health health; // Reference to the Health component (can be assigned in the inspector or will be auto-assigned in Awake)

    // Cache the Health component if not assigned in the inspector
    private void Awake()
    {
        if (!health) health = GetComponent<Health>();
    }

    // Subscribe to health events when the component is enabled
    private void OnEnable()
    {
        // If health reference is missing, do not subscribe to events
        if (!health) return;
        // Subscribe to health change and death events
        health.Changed += OnChanged;
        // Subscribe to the Died event to trigger player death logic
        health.Died += OnDied;
        // Trigger an initial health change event to update any UI or systems that rely on the player's health status at the start
        OnChanged();
    }

    // Unsubscribe from health events when the component is disabled to prevent memory leaks and unintended behavior
    private void OnDisable()
    {
        // If health reference is missing, do not attempt to unsubscribe from events
        if (!health) return;
        // Unsubscribe from health change and death events
        health.Changed -= OnChanged;
        // Unsubscribe from the Died event to prevent triggering player death logic when the component is disabled
        health.Died -= OnDied;
    }

    // This method is called whenever the health changes, and it triggers the OnHealthChanged event in the GameEvents system to notify other parts of the game about the new health status.
    private void OnChanged()
    {
        // Trigger the health changed event with the current health and maximum health values
        GameEvents.OnHealthChanged(health.CurrentHealth, health.MaxHealth);
    }

    // This method is called when the player dies, and it triggers the OnPlayerDied event in the GameEvents system to notify other parts of the game about the player's death.
    private void OnDied()
    {
        // Trigger the player died event to notify other systems that the player has died
        GameEvents.OnPlayerDied();
    }
}