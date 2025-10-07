using System;
using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;
using Zenject;

namespace Game.Scene.Portal
{
    public class PortalEnd : MonoBehaviour
    {
        private GameStateMachine _gameStateMachine;


        [Inject]
        public void Construct(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        private void OnTriggerEnter(Collider other)
        {
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}