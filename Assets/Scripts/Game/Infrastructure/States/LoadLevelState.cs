using Game.Infrastructure.Scene;
using Game.Infrastructure.States.StateMachine;

namespace Game.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter(string stateName)
        {
            _sceneLoader.Load(stateName, OnLoadComplete);
        }

        private void OnLoadComplete()
        {
            _gameStateMachine.Enter<InitializeGameplayState>();
        }

        public void Exit()
        {
        }
    }
}