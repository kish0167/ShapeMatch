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
        private RegenerationService _regenerationService;

        #endregion

        #region Setup/Teardown

        [Inject]
        private void Construct(CoinsService coinsService, ActionBarService actionBarService,
            RegenerationService regenerationService)
        {
            _coinsService = coinsService;
            _actionBarService = actionBarService;
            _regenerationService = regenerationService;
        }

        #endregion

        #region Public methods

        public override void Enter()
        {
            _coinsService.SolvingEnabled = true;
            _actionBarService.OnGameOverConditionMet += GameOverConditionMetCallback;
            _actionBarService.OnWinConditionMet += WinConditionMetCallback;
            _regenerationService.OnRegeneratingStarted += RegenerationStartedCallback;
        }

        public override void Exit()
        {
            _coinsService.SolvingEnabled = false;
            _actionBarService.OnGameOverConditionMet -= GameOverConditionMetCallback;
            _actionBarService.OnWinConditionMet -= WinConditionMetCallback;
            _regenerationService.OnRegeneratingStarted -= RegenerationStartedCallback;
        }

        #endregion

        #region Private methods

        private void GameOverConditionMetCallback()
        {
            StateMachine.TransitionTo<GameOverState>();
        }

        private void RegenerationStartedCallback()
        {
            StateMachine.TransitionTo<ReGeneratingFieldState>();
        }

        private void WinConditionMetCallback()
        {
            StateMachine.TransitionTo<WinState>();
        }

        #endregion
    }
}