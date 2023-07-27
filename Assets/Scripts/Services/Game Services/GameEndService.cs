using Features.Services.UI.Windows;
using UnityEngine;
using Features.Services.UI.Factory;
using Zenject;

public class GameEndService
{
    private readonly IWindowsService windowsService;
    private readonly Health playerHP;
    private readonly LevelManager levelManager;

    [Inject]
    public GameEndService(IWindowsService windowsService, PlayerMove player, LevelManager levelManager)
    {
        this.windowsService = windowsService;
        playerHP = player.GetComponent<Health>();
        this.levelManager = levelManager;
        Subscribe();
    }

    private void Subscribe()
    {
        playerHP.IsDead += ShowGameOverScreen;
        levelManager.FinishGame += ShowFinishScreen;
    }

    private void ShowFinishScreen()
    {
        windowsService.Open(WindowId.GameFinish);
        PauseGame();
        Cleanup();
    }

    private void ShowGameOverScreen()
    {
        windowsService.Open(WindowId.GameOver);
        PauseGame();
        Cleanup();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void Cleanup()
    {
        playerHP.IsDead -= ShowGameOverScreen;
        levelManager.FinishGame -= ShowFinishScreen;
    }
}
