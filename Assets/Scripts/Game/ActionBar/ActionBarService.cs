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

        #endregion

        #region Events

        public event Action OnActionBarFull;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(ActionBar actionBar)
        {
            _actionBar = actionBar;
            UpdateBar();
        }

        #endregion

        #region Public methods

        public bool TryPlaceInActionBar(Coin coin)
        {
            if (_coins.Count >= _actionBar.Capacity)
            {
                return false;
            }

            _coins.Add(coin);
            UpdateBar();

            if (CheckForTriplet())
            {
                _coins.RemoveRange(_coins.Count - 3, 3);
                UpdateBar();
            }
            else if (GameOverCondition())
            {
                OnActionBarFull?.Invoke();
            }

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
            return _coins.Count == _actionBar.Capacity;
        }

        private void UpdateBar()
        {
            _actionBar.Set(_coins);
        }

        #endregion
    }
}