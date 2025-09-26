using System;
using System.Collections.Generic;

namespace Game.Infrastructure.States.StateMachine
{
    public class GameStateMachine
    {   
        private IExitableState _currentState;
        private Dictionary<Type, IExitableState> _states;

        public GameStateMachine()
        {
            _states = new Dictionary<Type, IExitableState>();
        }
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        public void RegisterState<TState>(TState state) where TState : class, IExitableState
        {
            _states.Add(typeof(TState), state);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }


 
}