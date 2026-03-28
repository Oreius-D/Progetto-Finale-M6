using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int Total { get; private set; }
    public int Killed { get; private set; }

    private readonly HashSet<Health> tracked = new();

    private void Start()
    {
        // Register all enemies existing and pushUI
        RegisterAllEnemies();
        PushUI();
    }

    private void RegisterAllEnemies()
    {
        // Cerca tutti gli Enemy sotto questo parent
        var enemies = GetComponentsInChildren<EnemyMovement>(true);

        Total = enemies.Length;
        Killed = 0;
        tracked.Clear();

        for (int i = 0; i < enemies.Length; i++)
        {
            var enemy = enemies[i];
            if (!enemy) continue;

            // Health puo' stare sullo stesso GO o su child/parent
            var h = enemy.GetComponentInChildren<Health>(true);
            if (!h) continue;

            if (tracked.Add(h))
                h.Died += OnEnemyDied;
        }
    }

    private void OnEnemyDied()
    {
        Killed = Mathf.Min(Total, Killed + 1);
        PushUI();

        if (Killed >= Total)
            GameEvents.OnVictory();
    }

    private void PushUI()
    {
        GameEvents.OnEnemyCountChanged(Killed, Total);
    }

    private void OnDestroy()
    {
        // cleanup subscriptions
        foreach (var h in tracked)
        {
            if (h) h.Died -= OnEnemyDied;
        }
        tracked.Clear();
    }


    // ToDo: Aggiungere meccanica per registrare nemici che spawnano a runtime
}
