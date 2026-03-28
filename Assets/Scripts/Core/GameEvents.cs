using System;
using UnityEngine;

// A static class to hold game events. This class can be used for defining events that can be subscribed to by other classes, allowing for a decoupled event system.
public static class GameEvents
{
    // Health Events
    public static event Action<int, int> healthChanged; // Event for when health changes, with current and max health as parameters.
    public static event Action PlayerDied; // Event for when the player dies.

    // Weapon Events, also for UI updates
    public static event Action<Sprite> weaponChanged; // Event for when the weapon changes, with the new weapon name as a parameter.

    // Strctly UI Events
    public static event Action<int, int> enemyCountChanged; // killed, total
    public static event Action victory;

    //Todo ADD Dash Events, for when the player dashes, and when the dash is ready again.

    // Methods to invoke the events, which can be called from other classes to trigger the events.
    public static void OnHealthChanged(int currentHealth, int maxHealth) => healthChanged?.Invoke(currentHealth, maxHealth);
    public static void OnPlayerDied() => PlayerDied?.Invoke();
    public static void OnWeaponChanged(Sprite newWeapon) => weaponChanged?.Invoke(newWeapon);
    public static void OnEnemyCountChanged(int killed, int total) => enemyCountChanged?.Invoke(killed, total);
    public static void OnVictory() => victory?.Invoke();
}
