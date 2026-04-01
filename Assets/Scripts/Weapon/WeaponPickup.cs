using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WeaponPickup is a component that can be attached to a game object to make it a weapon pickup. When the player collides with the pickup, it will equip the specified weapon and optionally destroy the pickup object.
public class WeaponPickup : MonoBehaviour
{
    // The weapon definition to be equipped when the player picks up this item.
    [SerializeField] private WeaponDefinition weapon;
    // Whether to destroy the pickup object after the player picks it up.
    [SerializeField] private bool destroyOnPickup = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player. If not, do nothing.
        if (!other.CompareTag("Player")) return;

        // Try to get the WeaponController component from the player. If it doesn't exist, do nothing.
        var wc = other.GetComponent<WeaponController>();
        // If the player has a WeaponController and a valid weapon definition, equip the weapon. Otherwise, do nothing.
        if (!wc || !weapon) return;

        // Equip the weapon on the player's WeaponController.
        wc.Equip(weapon);

        // If destroyOnPickup is true, destroy this pickup object after equipping the weapon.
        if (destroyOnPickup)
            Destroy(gameObject);
    }
}
