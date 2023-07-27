using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private List<BaseEnemy> enemyPrefabs;
    private CellManager cellManager;
    private PlayerCoins playerCoins;

    public EnemyFactory(CellManager cellManager, PlayerCoins playerCoins)
    {
        this.playerCoins = playerCoins;
        this.cellManager = cellManager;
        enemyPrefabs = Resources.Load<EnemyPrefabs>(GlobalConst.EnemiesPath).Enemies;
    }

    public List<BaseEnemy> CreateNewEnemies(int enemiesCount, float enemyModifier)
    {
        List<BaseEnemy> newEnemies = new List<BaseEnemy>(enemiesCount);
        Vector3[] points = cellManager.GetEmptyCells(enemiesCount);
        for (int i = 0; i < enemiesCount; i++)
        {
            int enemyType = Random.Range(0, enemyPrefabs.Count);
            points[i].y = 0;
            BaseEnemy newEnemy = Object.Instantiate(enemyPrefabs[enemyType], points[i], Quaternion.identity);
            newEnemy.Init(enemyModifier, playerCoins);
            newEnemies.Add(newEnemy);
        }
        return newEnemies;
    }
}
