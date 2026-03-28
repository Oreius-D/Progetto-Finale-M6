using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEnemyCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        if (!text) text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        GameEvents.enemyCountChanged += OnChanged;
    }

    private void OnDisable()
    {
        GameEvents.enemyCountChanged -= OnChanged;
    }

    private void OnChanged(int killed, int total)
    {
        text.text = $"{killed}/{total}";
    }
}
