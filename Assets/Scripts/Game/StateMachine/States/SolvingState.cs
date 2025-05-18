using Game.ActionBar;
using Game.Coins;
using Zenject;

namespace Game.StateMachine.States
{
    public class SolvingState : GameState
    {
        #region Variables

        private ActionBarService _actionBarService;
        private CoinsService _coinsService;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinsService coinsService, ActionBarService actionBarService)
        {
            _coinsService = coinsService;
            _actionBarService = actionBarService;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _coinsService.SolvingEnabled = true;
            _actionBarService.OnActionBarFull += ActionBarFullCallback;
        }

        public override void Exit()
        {
            _coinsService.SolvingEnabled = false;
            _actionBarService.OnActionBarFull -= ActionBarFullCallback;
        }

        #endregion

        #region Private methods

        private void ActionBarFullCallback()
        {
            StateMachine.TransitionTo<GameOverState>();
        }

        #endregion
    }
}