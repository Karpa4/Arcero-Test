using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner
{
    private readonly int cellCountWidth;
    private readonly int cellCountHeight;
    private int obstacleCount;
    private GameObject prefab;
    private List<GameObject> activeObstacle;

    public ObstacleSpawner(int cellCountWidth, int cellCountHeight)
    {
        prefab = Resources.Load<GameObject>(GlobalConst.WallPath);
        this.cellCountWidth = cellCountWidth;
        this.cellCountHeight = cellCountHeight;
        GenerateObstacle();
    }

    public List<Vector3> SpawnObstacles()
    {
        List<Vector3> result = new List<Vector3>();
        for (int i = 0; i < obstacleCount; i++)
        {
            Vector3 newPoint;
            do
            {
                newPoint = GetRandomPoint();
            }
            while (result.Contains(newPoint));

            activeObstacle[i].transform.position = newPoint;
            result.Add(newPoint);
        }

        return result;
    }
    
    private void GenerateObstacle()
    {
        obstacleCount = (int)(cellCountWidth * cellCountHeight * GlobalConst.ObstacleCountModifier);
        activeObstacle = new List<GameObject>();
        for (int i = 0; i < obstacleCount; i++)
        {
            GameObject newObstacle = Object.Instantiate<GameObject>(prefab);
            activeObstacle.Add(newObstacle);
        }
    }

    public Vector3 GetRandomPoint()
    {
        int x = Random.Range(-cellCountWidth / 2 + 2, cellCountWidth / 2 - 1);
        int z = Random.Range(-cellCountHeight / 2 + 4, cellCountHeight / 2 - 2);

        Vector3 point = new Vector3(x, 0, z);
        return point;
    }
}
