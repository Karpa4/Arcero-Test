using System;
using UnityEngine;

public class PlayerCoins
{
    private int coins;
    private bool isMainCoins;
    public int Coins => coins;
    public event Action<int> CoinsChanged;

    public PlayerCoins(bool isMainCoins = true)
    {
        this.isMainCoins = isMainCoins;
        if (isMainCoins)
        {
            coins = PlayerPrefs.GetInt(GlobalConst.CoinsKey, GlobalConst.StartCoins);
        }
        else
        {
            coins = 0;
        }
    }

    public void AddCoins(int coinsCount)
    {
        coins += coinsCount;
        CoinsChanged?.Invoke(coins);
        if (isMainCoins)
        {
            Save();
        }
    }

    public void SpendCoins(int price)
    {
        if (price <= coins)
        {
            coins -= price;
            CoinsChanged?.Invoke(coins);
            Save();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt(GlobalConst.CoinsKey, coins);
    }
}
