using System.Collections.Generic;
using Game.ActionBar;
using Zenject;

namespace Game.Coins
{
    public class CoinsService
    {
        #region Variables

        private ActionBarService _actionBarService;

        private readonly List<Coin> _activeCoins = new();
        private CoinFactory _factory;

        public bool SolvingEnabled { get; set; } = false;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinFactory factory, ActionBarService service)
        {
            _actionBarService = service;
            _factory = factory;
            _factory.OnCoinSpawned += CoinSpawnedCallback;
            _factory.OnCoinDeSpawned += CoinDeSpawnedCallback;
        }

        #endregion

        #region Private methods

        private void CoinClickedCallback(Coin coin)
        {
            if (!SolvingEnabled)
            {
                return;
            }
            
            if (!_actionBarService.TryPlaceInActionBar(coin))
            {
                return;
            }

            _factory.DeSpawnCoin(coin);
        }

        private void CoinDeSpawnedCallback(Coin coin)
        {
            _activeCoins.Remove(coin);
            coin.ClickDetector.OnClicked -= CoinClickedCallback;
        }

        private void CoinSpawnedCallback(Coin coin)
        {
            _activeCoins.Add(coin);
            coin.ClickDetector.OnClicked += CoinClickedCallback;
        }

        #endregion
    }
}