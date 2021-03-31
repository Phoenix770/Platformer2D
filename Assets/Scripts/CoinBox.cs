using System;
using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int _totalCoinsCount = 3;
    int _remainingCoins;

    protected override bool CanUse => _remainingCoins > 0;

    private void Start()
    {
        _remainingCoins = _totalCoinsCount;
    }
     
    protected override void Use()
    {
        Coin.CoinsCollected++;
        _remainingCoins--;
    }
}
