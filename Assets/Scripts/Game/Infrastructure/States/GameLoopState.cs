using System;
using Data;
using Game.CameraLogic;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Signals;
using Game.Infrastructure.States.StateMachine;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICameraFollow _cameraFollow;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly SignalBus _signalBus;


        public GameLoopState(IPersistentProgressService persistentProgressService, SignalBus  signalBus)
        {
            _persistentProgressService = persistentProgressService;
            _signalBus = signalBus;
        }
        public void Enter()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Exit()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        private void OnPlayerDied(PlayerDiedSignal signal)
        {
            var player = signal.PlayerGameObject.GetComponent<PlayerMove>();
            player.Warp(_persistentProgressService.Progress.WorldData.PositionOnLevel.Position);
        }
    }
                
}