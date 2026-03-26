using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Player reference
    [SerializeField] private Transform playerTransform;

    // Player reference property
    public Transform Player => playerTransform;

    // Lists used for managing enemies
    private readonly List<EnemyMovement> activeEnemies = new();
    public IReadOnlyList<EnemyMovement> ActiveEnemies => activeEnemies;



    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        if(!playerTransform)
        {
            var go = GameObject.FindGameObjectWithTag("Player");
            if(go) playerTransform = go.transform;
        }
    }

    // Todo: Add methods to manage enemies, such as spawning, tracking, and removing them from the scene.

    // Method to register a new enemy (could be called by enemy scripts on Start)
    public void RegisterEnemy(EnemyMovement enemy)
    {
        // Check if enemy give in is not null and not already in the list
        if(!enemy) return;
        if(!activeEnemies.Contains(enemy)) activeEnemies.Add(enemy);
    }

    // Method to unregister an enemy (could be called by enemy scripts on OnDestroy)
    public void UnregisterEnemy(EnemyMovement enemy)
    {
        if(!enemy) return;
        activeEnemies.Remove(enemy);
    }

    //Method to find the closest enemy to the player
    public EnemyMovement GetClosestEnemy(Vector2 from, float range)
    {
        // Square the range to avoid unnecessary square root calculations
        float rangeSqr = range * range;
        // Square the player's position for distance calculations
        EnemyMovement closest = null;
        // Initialize closest distance to the maximum range squared
        float closestDistSqr = rangeSqr; 

        foreach (var enemy in activeEnemies)
        {
            // Check if the enemy is active and is active and enabled
            if (!enemy || !enemy.isActiveAndEnabled) continue;

            // Ignore dead enemies
            if (enemy.GetComponent<Health>() is Health health && health.IsDead) continue;

            // Calculate the squared distance from the enemy to the player
            float distSqr = ((Vector2)enemy.transform.position - from).sqrMagnitude;
            // If the distance is within the range and closer than the closest found so far, update closest
            if (distSqr < closestDistSqr)
            {
                closest = enemy;
                closestDistSqr = distSqr;
            }

        }
        return closest;
    }
}
