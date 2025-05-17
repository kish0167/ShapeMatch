using System.Collections.Generic;
using Game.StateMachine.States;
using UnityEngine;
using Utility;
using Zenject;

namespace Game.StateMachine
{
    public class LocalStateMachine : MonoBehaviour
    {
        #region Variables

        private readonly List<GameState> _states = new();

        private GameState _currentState;

        #endregion

        #region Setup/Teardown

        [Inject]
        public void Construct(BootstrapState bootstrapState, GameOverState gameOverState,
            GeneratingFieldState generatingFieldState, SolvingState solvingState, WinState winState,
            ReGeneratingFieldState reGeneratingFieldState)
        {
            _states.Add(bootstrapState);
            _states.Add(gameOverState);
            _states.Add(generatingFieldState);
            _states.Add(solvingState);
            _states.Add(winState);
            _states.Add(reGeneratingFieldState);

            foreach (GameState state in _states)
            {
                state.StateMachine = this;
            }
        }

        #endregion

        #region Unity lifecycle

        private void Start()
        {
            _currentState = new BootstrapState();
            _currentState.Enter();
            TransitionTo<GeneratingFieldState>();
        }

        #endregion

        #region Public methods

        public bool Is<T>() where T : GameState
        {
            return typeof(T) == _currentState.GetType();
        }

        public void TransitionTo<T>() where T : GameState
        {
            _currentState.Exit();

            foreach (GameState state in _states)
            {
                if (state.GetType() != typeof(T))
                {
                    continue;
                }

                _currentState = state;
                state.Enter();
                return;
            }

            this.Error($"Sate {nameof(T)} not present in state list");
        }

        #endregion
    }
}