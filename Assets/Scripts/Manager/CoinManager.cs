using System;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private int coinCount;
    public int CoinCount => coinCount;

    public event Action OnCoinCollect; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddCoin(int amount = 1)
    {
        coinCount += amount;
        OnCoinCollect?.Invoke();
    }
}
