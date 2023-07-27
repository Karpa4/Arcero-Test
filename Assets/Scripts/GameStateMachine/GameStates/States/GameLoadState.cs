using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;
using Features.Services;
using Zenject;

namespace Features.GameStates.States
{
    public class GameLoadState : IState
    {
        private readonly IGameStateMachine gameStateMachine;
        private readonly ISceneLoader sceneLoader;
        private readonly IWindowsService windowsService;
        private readonly IPlayerStaticData playerStaticData;

        [Inject]
        public GameLoadState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IWindowsService windowsService
            , IPlayerStaticData playerStaticData)
        {
            this.playerStaticData = playerStaticData;
            this.gameStateMachine = gameStateMachine;
            this.sceneLoader = sceneLoader;
            this.windowsService = windowsService;
            gameStateMachine.Register(this);
        }

        public void Exit()
        {

        }

        private void OnLoad()
        {
            CreateHUD();
            gameStateMachine.Enter<GameLoopState>();
        }

        private void CreateHUD() =>
          windowsService.Open(WindowId.HUD);

        public void Enter()
        {
            sceneLoader.Load(GlobalConst.GameSceneIndex, OnLoad);
        }
    }
}