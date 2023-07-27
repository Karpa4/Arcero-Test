using Features.UI.Windows.Base;
using UnityEngine;
using Features.GameStates.States;
using UnityEngine.UI;
using Zenject;
using Features.GameStates;

public class PauseWindow : BaseWindow
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine)
    {
        this.gameStateMachine = gameStateMachine;
    }

    protected override void Subscribe()
    {
        continueButton.onClick.AddListener(Continue);
        menuButton.onClick.AddListener(GoToMainMenu);
    }

    private void GoToMainMenu()
    {
        Cleanup();
        gameStateMachine.Enter<MainMenuState>();
    }

    private void Continue()
    {
        Cleanup();
        Destroy(gameObject);
    }

    protected override void Cleanup()
    {
        Time.timeScale = 1;
        continueButton.onClick.RemoveListener(Continue);
        menuButton.onClick.RemoveListener(GoToMainMenu);
    }
}
