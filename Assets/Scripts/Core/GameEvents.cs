using System;

// A static class to hold game events. This class can be used for defining events that can be subscribed to by other classes, allowing for a decoupled event system.
public static class GameEvents
{
    // Health Events
    public static event Action<int, int> healthChanged; // Event for when health changes, with current and max health as parameters.
    public static event Action PlayerDied; // Event for when the player dies.

    // Weapon Events, also for UI updates
    public static event Action<string> weaponChanged; // Event for when the weapon changes, with the new weapon name as a parameter.
    public static event Action<int, int> ammoChanged; // Event for when ammo changes, with current and max ammo as parameters.
    public static event Action<float> reloadStarted; // Event for when reloading starts, with the reload time as a parameter.

    // Strctly UI Events
    public static event Action<string> updateScore; // Event for updating the score, with the new score as a parameter.
    public static event Action<int> coinCollected; // Event for when a coin is collected, with the total coins as a parameter.

    //Todo ADD Dash Events, for when the player dashes, and when the dash is ready again.

    // Methods to invoke the events, which can be called from other classes to trigger the events.
    public static void OnHealthChanged(int currentHealth, int maxHealth) => healthChanged?.Invoke(currentHealth, maxHealth);
    public static void OnPlayerDied() => PlayerDied?.Invoke();
    public static void OnWeaponChanged(string newWeapon) => weaponChanged?.Invoke(newWeapon);
    public static void OnAmmoChanged(int currentAmmo, int maxAmmo) => ammoChanged?.Invoke(currentAmmo, maxAmmo);
    public static void OnReloadStarted(float reloadTime) => reloadStarted?.Invoke(reloadTime);
    public static void OnUpdateScore(string newScore) => updateScore?.Invoke(newScore);
    public static void OnCoinCollected(int totalCoins) => coinCollected?.Invoke(totalCoins);
}
