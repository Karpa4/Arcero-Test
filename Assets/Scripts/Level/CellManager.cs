using System.Collections.Generic;
using UnityEngine;

public class CellManager
{
    private int cellCountWidth;
    private int cellCountHeight;
    private ObstacleSpawner obstacleSpawner;
    private List<Vector3> activeObstacles;

    public CellManager(int cellCountWidth, int cellCountHeight)
    {
        this.cellCountWidth = cellCountWidth;
        this.cellCountHeight = cellCountHeight;
        obstacleSpawner = new ObstacleSpawner(cellCountWidth, cellCountHeight);
        CreateLevelWalls();
        SpawnNewObstacles();
    }

    private void CreateLevelWalls()
    {
        GameObject prefabWall = Resources.Load<GameObject>(GlobalConst.WallPath);
        float halfWidth = (float)cellCountWidth / 2 - 0.5f;
        float halfHeight = (float)cellCountHeight / 2;

        for (int i = 0; i < cellCountHeight; i++)
        {
            Object.Instantiate(prefabWall, new Vector3(halfWidth, 0, -halfHeight + i), Quaternion.identity);
            Object.Instantiate(prefabWall, new Vector3(-halfWidth, 0, -halfHeight + i), Quaternion.identity);
        }

        for (int i = 0; i < cellCountWidth - 2; i++)
        {
            float x = -halfWidth + 1 + i;
            Object.Instantiate(prefabWall, new Vector3(x, 0, halfHeight-1), Quaternion.identity);
            Object.Instantiate(prefabWall, new Vector3(x, 0, -halfHeight), Quaternion.identity);
        }
    }

    public void SpawnNewObstacles()
    {
        activeObstacles = obstacleSpawner.SpawnObstacles();
    }

    public Vector3[] GetEmptyCells(int cellCount)
    {
        Vector3[] result = new Vector3[cellCount];

        for (int i = 0; i < cellCount; i++)
        {
            Vector3 newPoint;
            do
            {
                newPoint = obstacleSpawner.GetRandomPoint();
            }
            while (activeObstacles.Contains(newPoint));

            result[i] = newPoint;
        }

        return result;
    }
}
