using UnityEngine;

public static class GlobalConst
{
    public static readonly Vector3 MapCenter = new Vector3(0, 0, 0);
    public const float ObstacleCountModifier = 0.04f;

    public const float startPlayerOffset = 2;
    public const float startPlayerPosY = 0.5f;
    public const float TriggerOffset = 1.5f;
    public const float EnemySpawnOffsetZ = 5;

    public const int GameSceneIndex = 2;
    public const int MenuSceneIndex = 1;

    public const string WallPath = "Game/Wall";
    public const string EndTriggerPath = "Game/EndTrigger";
    public const string EnemiesPath = "Enemies";
    public const string WindowsDataPath = "Windows/WindowsStaticData";
    public const string UIRootPath = "Windows/UIRoot";

    public const string CoinsKey = "Coins";
    public const int StartCoins = 0;
}
