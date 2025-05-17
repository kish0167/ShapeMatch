using System.Collections.Generic;
using Game.Coins;
using Unity.VisualScripting;
using Zenject;

namespace Game.ActionBar
{
    public class ActionBarService
    {
        private ActionBar _actionBar;
        private List<Coin> _coins = new();

        [Inject]
        private void Construct(ActionBar actionBar)
        {
            _actionBar = actionBar;
            UpdateBar();
        }

        public void PlaceInActionBar(Coin coin)
        {
            if (_coins.Count >= _actionBar.Capacity)
            {
                return;
            }
            
            _coins.Add(coin);
            UpdateBar();
        }

        private void UpdateBar()
        {
            _actionBar.Set(_coins);
        }
    }
}