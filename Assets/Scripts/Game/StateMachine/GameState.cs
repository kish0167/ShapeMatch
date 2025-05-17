namespace Game.StateMachine
{
    public abstract class GameState
    {
        public LocalStateMachine StateMachine { get; set; }

        public abstract void Enter();
        public abstract void Exit();
    }
}