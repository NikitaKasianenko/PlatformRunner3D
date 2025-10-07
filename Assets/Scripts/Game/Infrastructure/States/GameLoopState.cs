using Game.CameraLogic;
using Game.Infrastructure.Coroutine;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Signals;
using Game.Infrastructure.States.StateMachine;
using Game.Player;
using Project.StaticData;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ICameraFollow _cameraFollow;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly SignalBus _signalBus;
        private readonly IStaticDataService _staticDataService;
        private bool _isRespawning;


        public GameLoopState(IPersistentProgressService persistentProgressService,ICameraFollow cameraFollow, SignalBus  signalBus,IStaticDataService staticDataService)
        {
            _persistentProgressService = persistentProgressService;
            _cameraFollow = cameraFollow;
            _signalBus = signalBus;
            _staticDataService = staticDataService;
        }
        public void Enter()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        public void Exit()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(OnPlayerDied);
        }

        private async void OnPlayerDied(PlayerDiedSignal signal)
        {
            if (_isRespawning)
                return;
            _isRespawning = true;
            
            var playerMove = signal.PlayerGameObject.GetComponent<PlayerMove>();
            var playerRagdoll = signal.PlayerGameObject.GetComponent<PlayerRagdoll>();
            
            _cameraFollow.Follow(playerRagdoll.transformToFollow);
            await playerRagdoll.ActivateRagdoll(_staticDataService.GameSettings.RespawnDuration);
            if(signal.PlayerGameObject == null)
            {
                _isRespawning = false;
                return;
            }
            _cameraFollow.Follow(signal.PlayerGameObject.transform);
            
            playerMove.Warp(_persistentProgressService.Progress.WorldData.GetPositionForLevel(GlobalUtils.CurrentLevel()));
            _isRespawning = false;
        }
    }
                
}