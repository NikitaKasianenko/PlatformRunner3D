using Game.Infrastructure.Scene;
using Game.Infrastructure.States.StateMachine;
using UI.Signals;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SignalBus _signalBus;
        private readonly SceneLoader _sceneLoader;

        public MainMenuState(GameStateMachine stateMachine,SignalBus signalBus, SceneLoader  sceneLoader)
        {
            _stateMachine = stateMachine;
            _signalBus = signalBus;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            
            if (GlobalUtils.CurrentLevel() != Initial)
            {
                _sceneLoader.Load(Initial,onLoaded: OnLoadComplete);
            }
            else
            {
                _signalBus.Subscribe<LevelChosenSignal>(HandleLevel);
            }
            
        }
        private void OnLoadComplete()
        {
            _signalBus.Subscribe<LevelChosenSignal>(HandleLevel);
        }
        
        private void HandleLevel(LevelChosenSignal level)
        {
            _stateMachine.Enter<LoadLevelState,string>(level.Name);
        }   

        public void Exit()
        {
            _signalBus.TryUnsubscribe<LevelChosenSignal>(HandleLevel);
        }
    }
}