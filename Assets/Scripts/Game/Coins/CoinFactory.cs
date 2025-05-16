using Lean.Pool;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Coins
{
    public class CoinFactory
    {
        public CoinBase SpawnCoin(Vector3 position,
            CoinBase basePrefab, Color color, Sprite letter)
        {
            return LeanPool.Spawn(basePrefab, position, Quaternion.identity);
        }
    }
}