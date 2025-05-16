using Game.Coins;
using Zenject;

namespace Game.StateMachine.States
{
    public class GeneratingFieldState : GameState
    {
        #region Variables

        private CoinSpawner _coinSpawner;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinSpawner coinSpawner)
        {
            _coinSpawner = coinSpawner;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _coinSpawner.StartSpawning();
        }

        public override void Exit()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}