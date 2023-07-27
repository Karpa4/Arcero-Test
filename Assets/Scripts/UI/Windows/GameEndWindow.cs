using UnityEngine;
using Features.GameStates;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Zenject;
using Features.GameStates.States;
using TMPro;

public class GameEndWindow : BaseWindow
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(ToMainMenu);
    }

    protected override void Cleanup()
    {
        Time.timeScale = 1;
        restartButton.onClick.RemoveListener(RestartLevel);
        menuButton.onClick.RemoveListener(ToMainMenu);
    }

    private void RestartLevel()
    {
        Cleanup();
        gameStateMachine.Enter<GameLoadState>();
    }

    private void ToMainMenu()
    {
        Cleanup();
        gameStateMachine.Enter<MainMenuState>();
    }
}
