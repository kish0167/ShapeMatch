using System;
using Lean.Pool;
using UnityEngine;

namespace Game.Coins
{
    public class CoinFactory
    {

        public event Action<Coin> OnCoinSpawned;
        public event Action<Coin> OnCoinDeSpawned;
        
        #region Public methods

        public Coin SpawnCoin(Vector3 position,
            Coin prefab, Color color, Sprite letter)
        {
            Coin coin = LeanPool.Spawn(prefab, position, Quaternion.identity);
            coin.Base.color = color;
            coin.Letter.sprite = letter;
            OnCoinSpawned?.Invoke(coin);
            return coin;
        }

        public void DeSpawnCoin(Coin coin)
        {
            LeanPool.Despawn(coin);
            OnCoinDeSpawned?.Invoke(coin);
        }

        #endregion
    }
}