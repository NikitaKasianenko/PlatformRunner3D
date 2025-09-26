using Data;
using Game.CameraLogic;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Game.Infrastructure.States
{
    public class InitializeGameplayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly ICameraFollow _cameraFollow;
        private readonly IProgressWatchersRegister _progressWatchersRegister;
        private readonly IPersistentProgressService _persistentProgressService;

        public InitializeGameplayState(GameStateMachine gameStateMachine,IGameFactory gameFactory, ICameraFollow  cameraFollow,
            IProgressWatchersRegister  progressWatchersRegister, IPersistentProgressService  persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _cameraFollow = cameraFollow;
            _progressWatchersRegister = progressWatchersRegister;
            _persistentProgressService = persistentProgressService;
        }
        public void Enter()
        {
            _progressWatchersRegister.CleanUp();
            
            SetupWorld();
            InformProgressReaders();
            
            
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void SetupWorld()
        {
            SetupPlayer();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _progressWatchersRegister.Readers)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private void SetupPlayer()
        {
            Vector3 spawnPosition = GetSpawnPosition();
            GameObject player = _gameFactory.CreatePlayer(spawnPosition);
            _cameraFollow.Follow(player.transform);
        }
        
        private Vector3 GetSpawnPosition()
        {
            Vector3Data savedPosition = _persistentProgressService.Progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
            {
                return savedPosition.AsUnityVector();
            }
            GameObject spawnPoint = GameObject.FindWithTag(Constants.SpawnPoint);
            if (spawnPoint != null)
            {
                _persistentProgressService.Progress.WorldData.PositionOnLevel.Position =
                    spawnPoint.transform.position.AsVectorData();
                return spawnPoint.transform.position;
            }
            return Vector3.zero;
        }


        public void Exit()
        {
        }
    }
}