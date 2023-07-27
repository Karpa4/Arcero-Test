using Features.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Features.Services;
using Zenject;
using TMPro;
using Features.GameStates.States;

namespace Features.UI.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI coinsText;

        private IGameStateMachine gameStateMachine;
        private IPlayerStaticData playerStaticData;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IPlayerStaticData playerStaticData)
        {
            this.gameStateMachine = gameStateMachine;
            this.playerStaticData = playerStaticData;
            coinsText.text = "Coins: " + playerStaticData.PlayerCoins.Coins.ToString();
        }

        protected override void Subscribe()
        {
            startButton.onClick.AddListener(StartGame);
            exitButton.onClick.AddListener(ExitGame);
        }

        protected override void Cleanup()
        {
            startButton.onClick.RemoveListener(StartGame);
            exitButton.onClick.RemoveListener(ExitGame);
        }

        public void StartGame()
        {
            gameStateMachine.Enter<GameLoadState>();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
