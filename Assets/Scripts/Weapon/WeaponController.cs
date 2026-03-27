using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // Reference to the enemy manager, which can be set in the Unity Inspector.
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private Transform muzzle;

    // Starting weapon definition, which can be set in the Unity Inspector. This is the weapon that the player will start with when the game begins.
    [SerializeField] private WeaponDefinition startingWeapon;

    // The current weapon definition that the player is using. This will be updated when the player picks up new weapons.
    public WeaponDefinition CurrentWeapon { get; private set; }

    // Private variable to track the time since the last shot was fired, used for managing the autofire rate of the weapon.
    private float nextFireTime;

    //Awake is called when the script instance is being loaded. Here we set the current weapon to the starting weapon defined in the Unity Inspector.
    private void Awake()
    {
        // Find enemy manager in the scene if it hasn't been assigned in the inspector
        if (!enemyManager) enemyManager = GetComponent<EnemyManager>();
        // If a starting weapon is defined, equip it as the current weapon.


        if (startingWeapon) Equip(startingWeapon);
    }

    // Update is called once per frame. Here we check if the player is holding down the fire button and if enough time has passed since the last shot was fired to allow for autofire.
    private void Update()
    {
        if (startingWeapon == null || enemyManager == null) return;
        // Check if the weapon can fire based on the autofire rate and if the fire button is being held down.
        if (Time.time >= nextFireTime)
        {
            Debug.Log("Trying to auto fire...");
            TryAutoFire();
        }
    }

    public void Equip(WeaponDefinition weapon)
    {
        CurrentWeapon = weapon;
        GameEvents.OnWeaponChanged(weapon.Icon); // utile per UI weapon name/icon dopo
    }

    private void TryAutoFire()
    {
        Debug.Log("Trying to auto fire 2...");
        Vector2 origin = muzzle ? (Vector2)muzzle.position : (Vector2)transform.position;

        Debug.Log("Finding closest enemy...");
        EnemyMovement target = enemyManager.GetClosestEnemy(origin, CurrentWeapon.Range);
        if (!target) return;

        nextFireTime = Time.time + 1f / Mathf.Max(0.01f, CurrentWeapon.AutofireRate);
        Debug.Log($"Auto firing at target: {target.gameObject.name} with weapon: {CurrentWeapon.WeaponName}");

        Vector2 targetPos = target.transform.position;
        Vector2 dir = (targetPos - origin).normalized;

        SpawnBullet(origin, dir);

        if (CurrentWeapon.FireSound && SoundFXManager.Instance)
            SoundFXManager.Instance.PlaySoundEffect(CurrentWeapon.FireSound, origin);
    }

    private void SpawnBullet(Vector2 origin, Vector2 dir)
    {
        Debug.Log("Spawning bullet...");
        if (!CurrentWeapon.ProjectilePrefab) return;
        Debug.Log($"Instantiating bullet prefab: {CurrentWeapon.ProjectilePrefab.name} at position: {origin + dir * 0.3f}");

        Bullet b = Instantiate(CurrentWeapon.ProjectilePrefab, origin + dir * 0.3f, Quaternion.identity);
        Debug.Log($"Setting bullet damage: {CurrentWeapon.ProjectileDamage} and speed: {CurrentWeapon.ProjectileSpeed}");
        b.SetDamage(CurrentWeapon.ProjectileDamage);
        Debug.Log($"Initializing bullet with direction: {dir} and speed: {CurrentWeapon.ProjectileSpeed}");
        b.Init(dir, CurrentWeapon.ProjectileSpeed);
    }
}
