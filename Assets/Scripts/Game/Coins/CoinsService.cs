using System.Collections.Generic;
using Game.ActionBar;
using Zenject;

namespace Game.Coins
{
    public class CoinsService
    {
        #region Variables

        private readonly List<Coin> _activeCoins = new();

        private ActionBarService _actionBarService;
        private CoinFactory _factory;

        #endregion

        #region Properties

        public int ActiveCoinsCount => _activeCoins.Count;

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

        #region Public methods

        public void DestroyActiveCoins()
        {
            int count = _activeCoins.Count;
            for (int i = 0; i < count; i++)
            {
                _factory.DespawnCoin(_activeCoins[0]);
            }
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

            _factory.DespawnCoinSoft(coin);
        }

        private void CoinDeSpawnedCallback(Coin coin)
        {
            if (!_activeCoins.Contains(coin))
            {
                return;
            }
            
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