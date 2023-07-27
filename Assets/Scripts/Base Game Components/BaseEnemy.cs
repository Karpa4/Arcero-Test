using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private InitAttack initAttack;
    [SerializeField] private int CoinsCountForKill;
    private PlayerCoins playerCoins;

    public event Action<BaseEnemy> EnemyDied;

    private void Start()
    {
        health.IsDead += EnemyIsDead;
    }

    private void EnemyIsDead()
    {
        EnemyDied?.Invoke(this);
        playerCoins.AddCoins(CoinsCountForKill);
        health.IsDead -= EnemyIsDead;
        Destroy(gameObject);
    }

    public void Init(float modifier, PlayerCoins playerCoins)
    {
        this.playerCoins = playerCoins;
        health.Init(modifier);
        initAttack.Init(modifier);
    }
}
