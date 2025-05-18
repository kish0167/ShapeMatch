using Game.UI;
using Zenject;

namespace Game.StateMachine.States
{
    public class GameOverState : GameState
    {
        private GameOverScreen _gameOverScreen;

        [Inject]
        private void Construct(GameOverScreen screen)
        {
            _gameOverScreen = screen;
        }
        
        public override void Enter()
        {
            _gameOverScreen.Enable();
        }

        public override void Exit()
        {
            _gameOverScreen.Disable();
        }
    }
}