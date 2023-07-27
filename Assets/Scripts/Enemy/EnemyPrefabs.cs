using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "Enemies")]
public class EnemyPrefabs : ScriptableObject
{
    [SerializeField] private List<BaseEnemy> enemies;

    public List<BaseEnemy> Enemies => enemies;
}
