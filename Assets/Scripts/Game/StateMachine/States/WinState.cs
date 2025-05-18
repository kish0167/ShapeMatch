using Game.UI;
using Zenject;

namespace Game.StateMachine.States
{
    public class WinState : GameState
    {
        private WinScreen _winScreen;

        [Inject]
        private void Construct(WinScreen screen)
        {
            _winScreen = screen;
        }
        public override void Enter()
        {
            _winScreen.Enable();
        }

        public override void Exit()
        {
            _winScreen.Disable();
        }
    }
}