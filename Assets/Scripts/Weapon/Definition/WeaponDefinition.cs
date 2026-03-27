using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponDefinition")]
public class WeaponDefinition : ScriptableObject
{
    // Info about the weapon, such as name and description, which can be set in the Unity Inspector.
    [Header("Weapon Info")]
    [SerializeField] private string weaponName = "Gun";

    // Autofire rate of the weapon, which can be set in the Unity Inspector. This value determines how fast the weapon can fire when the player holds down the fire button.
    [Header("Weapon Stats")]
    [SerializeField] private float autofireRate = 0.5f;
    [SerializeField] private float range = 10f;

    // Reference to the projectile prefab that this weapon will fire, which can be set in the Unity Inspector.
    [Header("Bullet Stats")]
    [SerializeField] private Bullet projectilePrefab;
    [SerializeField] private float projectileSpeed = 20f;
    [SerializeField] private int projectileDamage = 1;

    // Audio clip for the weapon firing sound, which can be set in the Unity Inspector.
    [Header("Audio")]
    [SerializeField] private AudioClip fireSound;

    // Sprite icon for UI render
    [Header("Sprite")]
    [SerializeField] private Sprite icon;

    public string WeaponName => weaponName;
    public float AutofireRate => autofireRate;
    public float Range => range;
    public Bullet ProjectilePrefab => projectilePrefab;
    public float ProjectileSpeed => projectileSpeed;
    public int ProjectileDamage => projectileDamage;
    public AudioClip FireSound => fireSound;
    public Sprite Icon => icon;
}
