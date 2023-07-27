using System;
using System.Collections.Generic;
using Zenject;

public class EnemyManager
{
    private readonly EnemyFactory enemyFactory;
    private readonly Random random;
    private float defaultModifier = 1;
    private TargetsChecker targetsChecker;

    public event Action LevelIsComplete;

    [Inject]
    public EnemyManager(CellManager cellManager, PlayerCoins playerCoins, PlayerMove playerMove)
    {
        targetsChecker = playerMove.GetComponent<TargetsChecker>();
        targetsChecker.LevelCleared += OnLevelCleared;
        random = new Random();
        enemyFactory = new EnemyFactory(cellManager, playerCoins);
    }

    public void CreatNewEnemyWave(int currentLevel)
    {
        float enemyModifier = defaultModifier + currentLevel / GameSettings.EnemyPowerModifier;
        int enemyCount = random.Next(GameSettings.MinEnemiesCount, GameSettings.MaxEnemiesCount);
        enemyFactory.CreateNewEnemies(enemyCount, enemyModifier);
    }

    private void OnLevelCleared()
    {
        LevelIsComplete?.Invoke();
    }

    public void CleanUp()
    {
        targetsChecker.LevelCleared -= OnLevelCleared;
    }
}
