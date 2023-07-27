using Features.SceneLoading.Scripts;
using Features.Services.Assets;
using Features.Services.Coroutine;
using Features.Services.StaticData;
using Features.Services.UI.Windows;
using Features.GameStates;
using Features.GameStates.States;
using Features.Services;
using UnityEngine;
using Zenject;

namespace Features.Bootstrapp
{
    public class ProjectBootstrapper : MonoInstaller, ICoroutineRunner
    {
        [SerializeField] private LoadingCurtain loadingCurtain;

        public override void Start()
        {
            base.Start();
            ResolveStateMachine();
        }

        public override void InstallBindings()
        {
            BindPlayerData();
            BindAssetProvider();
            BindStaticData();
            BindLoadingCurtain();
            BindCoroutineRunner();
            BindSceneLoader();
            BindWindowsService();
            BindStateMachine();
        }

        private void BindAssetProvider() =>
          Container.Bind<IAssetProvider>().To<AssetProvider>().FromNew().AsSingle();

        private void BindStaticData()
        {
            StaticDataService staticDataService = new StaticDataService();
            staticDataService.Load();

            Container.Bind<IStaticDataService>().To<StaticDataService>().FromInstance(staticDataService).AsSingle();
        }

        private void BindPlayerData()
        {
            Container.Bind<IPlayerStaticData>().To<PlayerStaticData>().FromNew().AsSingle().NonLazy();
        }

        private void BindLoadingCurtain() =>
          Container.Bind<LoadingCurtain>().To<LoadingCurtain>().FromComponentInNewPrefab(loadingCurtain).AsSingle();

        private void BindCoroutineRunner() =>
          Container.Bind<ICoroutineRunner>().To<ICoroutineRunner>().FromInstance(this).AsSingle();

        private void BindSceneLoader() =>
          Container.Bind<ISceneLoader>().To<SceneLoader>().FromNew().AsSingle();

        private void BindWindowsService() =>
          Container.Bind<IWindowsService>().To<WindowsService>().FromNew().AsSingle();

        private void BindStateMachine()
        {
            Container.Bind<GameLoadState>().To<GameLoadState>().FromNew().AsSingle();
            Container.Bind<GameLoopState>().To<GameLoopState>().FromNew().AsSingle();
            Container.Bind<MainMenuState>().To<MainMenuState>().FromNew().AsSingle();
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().FromNew().AsSingle();
            Container.Bind<Game>().To<Game>().FromNew().AsSingle();
        }

        private void ResolveStateMachine()
        {
            Container.Resolve<GameLoadState>();
            Container.Resolve<GameLoopState>();
            Container.Resolve<MainMenuState>();
            Container.Resolve<Game>().StartGame();
        }
    }
}