using Game.Infrastructure.Scene;
using Game.Infrastructure.States.StateMachine;

namespace Game.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine,SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded);
        }

        public void onLoaded()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }
        public void Exit()
        {
        }
    }
}