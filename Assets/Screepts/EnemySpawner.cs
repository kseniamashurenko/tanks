using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private EnemyStats enemyStats1;
    [SerializeField] private EnemyCont enemyPref;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private int _maxEnemy = 10;
    private List<int> slotsUsed = new();

    private void Start()
    {
        SpawnEnemy();
    }
    private void SpawnEnemy()
    {
        if (enemyPref == null) return;
        slotsUsed.Clear();
        for (int i = 0; i < _maxEnemy; i++)
        {
            int randomIndex = GetSlotForEnemy();
            Transform spawnPoint = spawnPoints[randomIndex];
            EnemyCont enemy = Instantiate(enemyPref, spawnPoint.position, spawnPoint.rotation);
            enemy.Initialize(enemyStats);
        }
    }
    private int GetSlotForEnemy()
    {
        int slot = 0;
        for(int j = 0; j<spawnPoints.Count; j++)
        {
            int randomIndex= Random.Range(0, spawnPoints.Count);
            if (!slotsUsed.Contains(randomIndex))
            {
                slot = randomIndex;
                slotsUsed.Add(randomIndex);
                return slot;
            }
        }
        return slot;
    }
}
