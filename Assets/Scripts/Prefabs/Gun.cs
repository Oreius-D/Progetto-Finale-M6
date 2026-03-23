using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float fireRate = 2f;        // Shots per second
    [SerializeField] float fireRange = 10f;      // Maximum firing range
    [SerializeField] Bullet bulletPrefab;        // Bullet prefab to instantiate
    [SerializeField] float bulletSpeed = 10f;    // Bullet movement speed
    [SerializeField] int bulletDamage = 1;       // Damage dealt by each bullet

    private float nextFireTime;                  // Time when the next shot is allowed

    private void Update()
    {
        // Automatically shoot at the nearest enemy
        ShootNearestEnemy();
    }

    /// <summary>
    /// Handles automatic shooting logic, respecting fire rate and range.
    /// </summary>
    private void ShootNearestEnemy()
    {
        // Respect fire rate
        if (Time.time < nextFireTime)
            return;

        // Find the closest enemy within range
        GameObject target = FindNearestEnemy();
        if (target == null)
            return;

        // Schedule next allowed shot
        nextFireTime = Time.time + 1f / fireRate;

        Vector2 myPos = transform.position;
        Vector2 targetPos = target.transform.position;

        // Direction from gun to target
        Vector2 dir = (targetPos - myPos).normalized;

        // Small offset to avoid spawning the bullet inside the player
        Vector2 spawnPos = myPos + dir * 0.3f;

        // Instantiate and initialize the bullet
        Bullet b = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
        b.SetDamage(bulletDamage);
        b.Init(dir, bulletSpeed);

        // Apply sorting order to the bullet based on the owner's SortingGroup
        var ownerGroup = GetComponentInParent<UnityEngine.Rendering.SortingGroup>();
        var bulletSR = b.GetComponent<SpriteRenderer>();

        if (ownerGroup != null && bulletSR != null)
        {
            // Keep the same sorting layer as the owner
            bulletSR.sortingLayerID = ownerGroup.sortingLayerID;

            // Render the bullet slightly in front of the player
            bulletSR.sortingOrder = ownerGroup.sortingOrder + 1;
        }
    }

    /// <summary>
    /// Finds the nearest active enemy within firing range.
    /// </summary>
    private GameObject FindNearestEnemy()
    {
        // Find all enemies using tag based lookup
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject nearest = null;
        float minDistSq = fireRange * fireRange;

        Vector2 myPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            // Skip invalid or inactive enemies
            if (enemy == null || !enemy.activeInHierarchy)
                continue;

            // Compute squared distance for performance
            float distSq = ((Vector2)enemy.transform.position - myPos).sqrMagnitude;

            // Keep the closest enemy within range
            if (distSq < minDistSq)
            {
                minDistSq = distSq;
                nearest = enemy;
            }
        }

        return nearest;
    }
}