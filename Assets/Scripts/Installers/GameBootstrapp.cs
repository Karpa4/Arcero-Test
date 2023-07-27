using UnityEngine;
using Zenject;
using Features.UI.Windows.Base;
using Features.Services.UI.Factory.BaseUI;
using UnityEngine.AI;

public class GameBootstrapp : MonoInstaller
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Transform planeGround;
    [SerializeField] private NavMeshSurface surface;

    public override void InstallBindings()
    {
        Vector3 startPlayerPos = CalcPlayerPos();
        playerMove.transform.position = startPlayerPos;
        playerMove.GetComponent<Health>().Init(1);

        planeGround.transform.localScale = new Vector3((float)GameSettings.CellWidth / 10, 1, (float)GameSettings.CellHeight / 10);

        BindNavmesh();
        BindGameCoins();
        BindCellManager();
        BindEnemyManager();
        BindLevelManager(startPlayerPos);
        BindPlayer();
        BindUI();
        BindEndGameService();
    }

    public override void Start()
    {
        base.Start();
        Container.Resolve<IUIFactory>();
        SetCameraFOW();
    }

    private Vector3 CalcPlayerPos()
    {
        Vector3 startPlayerPos = GlobalConst.MapCenter;
        startPlayerPos.y = GlobalConst.startPlayerPosY;
        startPlayerPos.z = startPlayerPos.z - (float)GameSettings.CellHeight / 2 + GlobalConst.startPlayerOffset;
        return startPlayerPos;
    }

    private void BindNavmesh()
    {
        Container.Bind<NavMeshSurface>().FromInstance(surface).AsSingle();
    }

    private void BindGameCoins()
    {
        Container.Bind<PlayerCoins>().FromNew().AsSingle().WithArguments<bool>(false).NonLazy();
    }

    private void BindLevelManager(Vector3 startPlayerPos)
    {
        Container.Bind<LevelManager>().FromNew().AsSingle().
            WithArguments<int, Vector3>(GameSettings.MaxLevel, startPlayerPos).NonLazy();
    }

    private void BindCellManager()
    {
        Container.Bind<CellManager>().FromNew().AsSingle().
            WithArguments<int, int>(GameSettings.CellWidth, GameSettings.CellHeight).NonLazy();
    }

    private void BindEnemyManager()
    {
        Container.Bind<EnemyManager>().FromNew().AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        Container.Bind<PlayerMove>().FromInstance(playerMove).AsSingle();
    }

    private void BindUI()
    {
        Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();
    }

    private void BindEndGameService()
    {
        Container.Bind<GameEndService>().FromNew().AsSingle().NonLazy();
    }

    private void SetCameraFOW()
    {
        float etalonW = 4;
        float etalonH = 10;
        float etalonModW = 10;
        float etalonModH = 4.15f;
        float modifierW = 0.24f;
        float modifierH = 0.035f;
        float widthFow = GameSettings.CellWidth * (etalonModW - (GameSettings.CellWidth - etalonW) * modifierW);
        float heightFow = GameSettings.CellHeight * (etalonModH - (GameSettings.CellHeight - etalonH) * modifierH);

        float fow = 0;
        if (widthFow > heightFow)
        {
            fow = widthFow;
        }
        else
        {
            fow = heightFow;
        }
        Camera.main.fieldOfView = fow;
    }
}