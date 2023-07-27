using UnityEngine;
using Features.GameStates;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Features.Services;
using Zenject;
using Features.GameStates.States;
using TMPro;

public class GameFinishWindow : BaseWindow
{
    [SerializeField] private TextMeshProUGUI endText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private IGameStateMachine gameStateMachine;

    [Inject]
    public void Construct(IGameStateMachine gameStateMachine, IPlayerStaticData playerStaticData, PlayerCoins playerCoins)
    {
        this.gameStateMachine = gameStateMachine;
        playerStaticData.PlayerCoins.AddCoins(playerCoins.Coins);
        ShowFinishStat(playerCoins.Coins);
    }

    private void ShowFinishStat(int coins)
    {
        endText.text = $"You earned {coins} coins";
    }

    protected override void Subscribe()
    {
        restartButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(ToMainMenu);
    }

    protected override void Cleanup()
    {
        Time.timeScale = 1;
        restartButton.onClick.RemoveListener(NextLevel);
        menuButton.onClick.RemoveListener(ToMainMenu);
    }

    private void NextLevel()
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
