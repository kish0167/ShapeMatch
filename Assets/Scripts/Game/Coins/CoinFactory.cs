using System;
using Lean.Pool;
using UnityEngine;

namespace Game.Coins
{
    public class CoinFactory
    {
        #region Events

        public event Action<Coin> OnCoinDeSpawned;

        public event Action<Coin> OnCoinSpawned;

        #endregion

        #region Public methods

        public void DespawnCoin(Coin coin)
        {
            LeanPool.Despawn(coin);
            OnCoinDeSpawned?.Invoke(coin);
        }

        public Coin SpawnCoin(Vector3 position,
            Coin prefab, Color color, Sprite letter)
        {
            Coin coin = LeanPool.Spawn(prefab, position, Quaternion.identity);
            coin.Base.color = color;
            coin.Letter.sprite = letter;
            OnCoinSpawned?.Invoke(coin);
            return coin;
        }

        public void DespawnCoinSoft(Coin coin)
        {
            coin.gameObject.SetActive(false);
            OnCoinDeSpawned?.Invoke(coin);
        }

        #endregion
    }
}