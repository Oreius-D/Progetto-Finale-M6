using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;

    private void Awake()
    {
        if (!iconImage) iconImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        GameEvents.weaponChanged += OnIconChanged;
    }

    private void OnDisable()
    {
        GameEvents.weaponChanged -= OnIconChanged;
    }

    private void OnIconChanged(Sprite icon)
    {
        if (!iconImage) return;

        iconImage.sprite = icon;
    }
}
