namespace Game.StateMachine
{
    public abstract class GameState
    {
        public abstract void Enter();
        public abstract void Exit();
    }
}