using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private WeaponDefinition weapon;
    [SerializeField] private bool destroyOnPickup = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        Debug.Log("Player entered weapon pickup trigger");

        var wc = other.GetComponent<WeaponController>();
        Debug.Log("WeaponController component found: " + (wc != null));
        if (!wc || !weapon) return;
        Debug.Log("Equipping weapon: " + weapon.name);

        wc.Equip(weapon);
        Debug.Log("Weapon equipped: " + weapon.name);

        if (destroyOnPickup)
            Destroy(gameObject);
    }
}
