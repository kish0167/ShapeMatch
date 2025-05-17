using UnityEngine;
using Zenject;

namespace Game.Coins
{
    public class CoinsService
    {
        private CoinSpawner _spawner;
        
        [Inject]
        private void Construct(CoinSpawner spawner)
        {
            _spawner = spawner;
        }
    }
}