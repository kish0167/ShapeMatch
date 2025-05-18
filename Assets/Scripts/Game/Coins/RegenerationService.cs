using System;
using System.Collections.Generic;
using Game.ActionBar;
using Game.UI;
using Zenject;

namespace Game.Coins
{
    public class RegenerationService
    {
        #region Variables

        private const int CoinsLeftLimit = 3;
        private ActionBarService _actionBarService;
        private RegenerateButton _button;
        private CoinsService _coinsService;
        private CoinSpawner _spawner;

        #endregion

        #region Events

        public event Action OnRegeneratingStarted;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(RegenerateButton button, CoinsService coinsService,
            ActionBarService actionBarService, CoinSpawner spawner)
        {
            _button = button;
            _button.Button.onClick.AddListener(Regenerate);
            _coinsService = coinsService;
            _actionBarService = actionBarService;
            _spawner = spawner;
        }

        #endregion

        #region Private methods

        private void Regenerate()
        {
            List<Coin> newCoinsQueue = _actionBarService.CreateMissingCoinsTypes();
            int coinsCount = _coinsService.ActiveCoinsCount - newCoinsQueue.Count;

            if (coinsCount < CoinsLeftLimit)
            {
                return;
            }

            _coinsService.DestroyActiveCoins();
            _spawner.SpawnRegenerated(newCoinsQueue, coinsCount);

            OnRegeneratingStarted?.Invoke();
        }

        #endregion
    }
}