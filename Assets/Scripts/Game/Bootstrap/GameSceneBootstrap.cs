using Game.Infrastructure.Factory;
using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;
using Zenject;

namespace Game.Bootstrap
{
    public class GameSceneBootstrap : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly InitializeGameplayState _initState;
        private readonly GameLoopState _loopState;

        public GameSceneBootstrap(
            GameStateMachine gameStateMachine, 
            InitializeGameplayState initState, 
            GameLoopState loopState)
        {
            _gameStateMachine = gameStateMachine;
            _initState = initState;
            _loopState = loopState;
        }

        public void Initialize()
        {
            _gameStateMachine.RegisterState(_initState);
            _gameStateMachine.RegisterState(_loopState);
        }
    }

}