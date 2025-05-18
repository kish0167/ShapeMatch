using System;
using System.Collections.Generic;
using Game.Coins;
using Zenject;

namespace Game.ActionBar
{
    public class ActionBarService
    {
        #region Variables

        private readonly List<Coin> _coins = new();
        private ActionBar _actionBar;

        private CoinsService _coinsService;
        private CoinFactory _factory;

        #endregion

        #region Events

        public event Action OnGameOverConditionMet;
        public event Action OnWinConditionMet;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(ActionBar actionBar, CoinsService coinsService, CoinFactory factory)
        {
            _coinsService = coinsService;
            _actionBar = actionBar;
            _factory = factory;
            UpdateBar();
        }

        #endregion

        #region Public methods

        public List<Coin> CreateMissingCoinsTypes()
        {
            List<Coin> coins = new();

            foreach (Coin coin in _coins)
            {
                coins.Add(coin);
                coins.Add(coin);
            }

            return coins;
        }

        public bool TryPlaceInActionBar(Coin coin)
        {
            if (_coins.Count >= _actionBar.Capacity)
            {
                return false;
            }

            _coins.Add(coin);

            if (CheckForTriplet())
            {
                int index = _coins.Count - 3;
                _factory.DespawnCoin(_coins[index]);
                _factory.DespawnCoin(_coins[index + 1]);
                _factory.DespawnCoin(_coins[index + 2]);
                _coins.RemoveRange(index, 3);
            }
            else if (GameOverCondition())
            {
                OnGameOverConditionMet?.Invoke();
            }

            if (WinCondition())
            {
                OnWinConditionMet?.Invoke();
            }

            UpdateBar();
            return true;
        }

        #endregion

        #region Private methods

        private bool CheckForTriplet()
        {
            if (_coins.Count < 3)
            {
                return false;
            }

            int lastIndex = _coins.Count - 1;
            Coin a = _coins[lastIndex];
            Coin b = _coins[lastIndex - 1];
            Coin c = _coins[lastIndex - 2];

            return CompareShapes(a, b, c) || CompareLetters(a, b, c) || CompareColors(a, b, c);
        }

        private bool CompareColors(Coin a, Coin b, Coin c)
        {
            return a.Base.color == b.Base.color && b.Base.color == c.Base.color;
        }

        private bool CompareLetters(Coin a, Coin b, Coin c)
        {
            return a.Letter.sprite == b.Letter.sprite && b.Letter.sprite == c.Letter.sprite;
        }

        private bool CompareShapes(Coin a, Coin b, Coin c)
        {
            return a.Base.sprite == b.Base.sprite && b.Base.sprite == c.Base.sprite;
        }

        private bool GameOverCondition()
        {
            return _coins.Count == _actionBar.Capacity ||
                   (_coinsService.ActiveCoinsCount == 0 && _coins.Count != 0);
        }

        private void UpdateBar()
        {
            _actionBar.Set(_coins);
        }

        private bool WinCondition()
        {
            return _coinsService.ActiveCoinsCount == 1 && _coins.Count == 0;
        }

        #endregion
    }
}