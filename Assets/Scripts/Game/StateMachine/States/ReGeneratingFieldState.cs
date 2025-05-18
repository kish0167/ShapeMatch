using Game.Coins;
using Zenject;

namespace Game.StateMachine.States
{
    public class ReGeneratingFieldState : GameState
    {
        #region Variables

        private CoinSpawner _spawner;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinSpawner spawner)
        {
            _spawner = spawner;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _spawner.OnSpawningEnded += SpawningEndedCallback;
        }

        public override void Exit()
        {
            _spawner.OnSpawningEnded -= SpawningEndedCallback;
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