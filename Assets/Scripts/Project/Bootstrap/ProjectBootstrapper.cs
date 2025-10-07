using System;
using Game.Infrastructure.Factory;
using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;
using Zenject;

namespace Project.Bootstrap
{
    public class ProjectBootstrapper : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;
        private StatesFactory _statesFactory;

        [Inject]
        public void Construct(GameStateMachine gameStateMachine, StatesFactory  statesFactory)
        {
            _statesFactory = statesFactory;
            _gameStateMachine = gameStateMachine;
        }

   

        private void Awake()
        {
            Debug.Log(">>> ProjectBootstrapper started");
            _gameStateMachine.RegisterState(_statesFactory.Create<BootstrapState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<MainMenuState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadProgressState>());
            _gameStateMachine.RegisterState(_statesFactory.Create<LoadLevelState>());
            _gameStateMachine.Enter<BootstrapState>();
            
            SceneAccess.SceneAccess.WasOnInitial = true;
            
            Application.targetFrameRate = 120;
            QualitySettings.vSyncCount = 0;
            
        }
        
        
    }
}