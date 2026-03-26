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

        var wc = other.GetComponent<WeaponController>();
        if (!wc || !weapon) return;

        wc.Equip(weapon);

        if (destroyOnPickup)
            Destroy(gameObject);
    }
}
