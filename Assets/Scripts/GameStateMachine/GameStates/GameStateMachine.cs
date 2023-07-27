using System;
using System.Collections.Generic;
using Features.GameStates.States.Interfaces;
using Zenject;

namespace Features.GameStates
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        [Inject]
        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>(5);

        }

        public void Register<TState>(TState state) where TState : class, IExitableState =>
          _states.Add(typeof(TState), state);

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public TState GetState<TState>() where TState : class, IExitableState =>
          _states[typeof(TState)] as TState;


        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        public void Cleanup()
        {

        }
    }
}