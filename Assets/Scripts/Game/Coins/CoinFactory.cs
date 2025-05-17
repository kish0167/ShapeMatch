using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Coins
{
    public class CoinFactory
    {
        public Coin SpawnCoin(Vector3 position,
            Coin prefab, Color color, Sprite letter)
        {
            Coin coin = LeanPool.Spawn(prefab, position, Quaternion.identity);
            coin.Base.color = color;
            coin.Letter.sprite = letter;
            return coin;
        }
        
        
    }
}