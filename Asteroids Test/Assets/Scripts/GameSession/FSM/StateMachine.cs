using System;
using System.Collections.Generic;

namespace GameSession.FSM
{
    public class StateMachine : IStateMachine, IDisposable
    {
        private Dictionary<Type, IState> _states;
        private IState _currentState;

        public StateMachine()
        {
            _states = new Dictionary<Type, IState>();
        }

        public void Register<T>(T state) where T : IState
        {
            Type stateType = typeof(T);

            if (!_states.ContainsKey(stateType))
            {
                _states.Add(stateType, state);
            }
        }

        public void Enter<T>() where T : IState
        {
            Type key = typeof(T);

            if (_states.ContainsKey(key) == false)
            {
                throw new Exception($"no rigister states : {key}");
            }
            
            _currentState.OnExit();
            _currentState = _states[key];
            _currentState.OnEnter();
        }

        public void Dispose()
        {
            _states.Clear();
            _currentState.OnExit();
        }
    }
}