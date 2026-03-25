using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Player reference
    [SerializeField] private Transform playerTransform;

    // Player reference property
    public Transform Player => playerTransform;

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
}
