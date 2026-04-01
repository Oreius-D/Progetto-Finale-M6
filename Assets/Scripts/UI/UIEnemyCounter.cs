using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyCounter : MonoBehaviour
{
    // Reference to the TextMeshPro component that displays the enemy count. If not set in the inspector, it will try to get it from the same GameObject.
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        // If the text reference is not set in the inspector, try to get it from the same GameObject.
        if (!text) text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        // Subscribe to the enemyCountChanged event when the GameObject is enabled.
        GameEvents.enemyCountChanged += OnChanged;
    }

    private void OnDisable()
    {
        // Unsubscribe from the enemyCountChanged event when the GameObject is disabled to prevent memory leaks.
        GameEvents.enemyCountChanged -= OnChanged;
    }

    private void OnChanged(int killed, int total)
    {
        // Update the text to show the current count of killed enemies and total enemies in the format "killed/total".
        text.text = $"{killed}/{total}";
    }
}
