using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    // Reference to the Image component that displays the weapon icon. If not set in the inspector, it will try to get it from the same GameObject.
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        // If the iconImage reference is not set in the inspector, try to get it from the same GameObject.
        if (!iconImage) iconImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        // Subscribe to the weaponChanged event to update the icon when the weapon changes.
        GameEvents.weaponChanged += OnIconChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the weaponChanged event when the GameObject is disabled to prevent memory leaks.
        GameEvents.weaponChanged -= OnIconChanged;
    }

    private void OnIconChanged(Sprite icon)
    {
        // If the iconImage reference is not set, do nothing.
        if (!iconImage) return;

        // Update the sprite of the iconImage to the new icon provided by the event.
        iconImage.sprite = icon;
    }
}
