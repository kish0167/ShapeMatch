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
            _coinSpawner.OnSpawningEnded += SpawningEndedCallback;
        }

        public override void Exit()
        {
            _coinSpawner.OnSpawningEnded -= SpawningEndedCallback;
        }

        #endregion

        #region Private methods

        private void SpawningEndedCallback()
        {
            StateMachine.TransitionTo<SolvingState>();
        }

        #endregion
    }
}