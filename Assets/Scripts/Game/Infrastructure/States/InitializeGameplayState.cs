using System.Collections.Generic;
using System.Linq;
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
        private readonly IEnumerable<ISaveProgressReader> _saveProgressReaders;

        public InitializeGameplayState(GameStateMachine gameStateMachine,IGameFactory gameFactory, ICameraFollow  cameraFollow,
            IProgressWatchersRegister  progressWatchersRegister, IPersistentProgressService  persistentProgressService, IEnumerable<ISaveProgressReader> saveProgressReaders)
        {
            _gameStateMachine = gameStateMachine;
            _gameFactory = gameFactory;
            _cameraFollow = cameraFollow;
            _progressWatchersRegister = progressWatchersRegister;
            _persistentProgressService = persistentProgressService;
            _saveProgressReaders = saveProgressReaders;
        }
        public void Enter()
        {
            _progressWatchersRegister.CleanUp();
            SetupWorld();
            RegisterSceneReaders();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void SetupWorld()
        {
            SetupPlayer();
            SetupHud();
        }

        private void SetupHud()
        {
            _gameFactory.CreateHud();
        }

        private void InformProgressReaders()
        {
            foreach (var progressReader in _progressWatchersRegister.Readers)
            {
                progressReader.LoadProgress(_persistentProgressService.Progress);
            }
        }

        private void RegisterSceneReaders()
        {

            foreach (var reader in _saveProgressReaders)
            {
                _progressWatchersRegister.Register(reader);
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
            string currentLevel = GlobalUtils.CurrentLevel();

            Vector3Data saved = _persistentProgressService.Progress.WorldData.GetPositionForLevel(currentLevel);
            if (saved != null)
                return saved.AsUnityVector();

            GameObject spawnPoint = GameObject.FindWithTag(Constants.SpawnPoint);
            if (spawnPoint != null)
            {
                _persistentProgressService.Progress.WorldData.SetPositionForLevel(
                    currentLevel, spawnPoint.transform.position.AsVectorData());
                return spawnPoint.transform.position;
            }

            return Vector3.zero;
        }


        public void Exit()
        {
        }
    }
}