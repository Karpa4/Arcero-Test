using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Features.Services.Coroutine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager
{
    private readonly int maxLevel;
    private readonly EnemyManager enemyManager;
    private readonly CellManager cellManager;
    private readonly NavMeshSurface surface;
    private int currentLevel;
    private EndLevelTrigger endLevelTrigger;
    private ICoroutineRunner runner;
    public event Action FinishGame;

    [Inject]
    public LevelManager(int maxLevel, Vector3 startPlayerPos, CellManager cellManager, 
        EnemyManager enemyManager, NavMeshSurface surface, ICoroutineRunner runner)
    {
        this.maxLevel = maxLevel;
        this.cellManager = cellManager;
        this.enemyManager = enemyManager;
        this.surface = surface;
        this.runner = runner;
        currentLevel = 0;

        surface.BuildNavMesh();
        enemyManager.LevelIsComplete += ActiveNewLevelTrigger;
        runner.StartCoroutine(DelayBeforeSpawn());

        CreateEndLevelTrigger(startPlayerPos);
    }

    private IEnumerator DelayBeforeSpawn()
    {
        yield return new WaitForSeconds(0.05f);
        enemyManager.CreatNewEnemyWave(currentLevel);
    }

    private void CreateEndLevelTrigger(Vector3 startPlayerPos)
    {
        EndLevelTrigger trigger = Resources.Load<EndLevelTrigger>(GlobalConst.EndTriggerPath);
        float triggerPosZ = (float)GameSettings.CellHeight / 2 - GlobalConst.TriggerOffset;
        endLevelTrigger = UnityEngine.Object.Instantiate(trigger, new Vector3(0, 0, triggerPosZ), Quaternion.identity);
        endLevelTrigger.Init(startPlayerPos);
        endLevelTrigger.LevelIsDone += GoToNextLevel;
        endLevelTrigger.gameObject.SetActive(false);
    }

    private void GoToNextLevel()
    {
        endLevelTrigger.gameObject.SetActive(false);
        currentLevel++;
        if (currentLevel >= maxLevel)
        {
            Debug.Log("FinishGame");
            enemyManager.CleanUp();
            FinishGame?.Invoke();
        }
        else
        {
            cellManager.SpawnNewObstacles();
            surface.BuildNavMesh();
            runner.StartCoroutine(DelayBeforeSpawn());
        }
    }

    private void ActiveNewLevelTrigger()
    {
        endLevelTrigger.gameObject.SetActive(true);
    }
}
