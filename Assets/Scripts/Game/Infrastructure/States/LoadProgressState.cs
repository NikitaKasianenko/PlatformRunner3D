using System.Collections.Generic;
using Data;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Game.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string InitialLevel = "Level1";
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private readonly IPlayerSettingsService _playerSettingsService;

        public LoadProgressState(GameStateMachine gameStateMachine,
            IPersistentProgressService  progressService,
            ISaveLoadService saveLoadService,
            IPlayerSettingsService playerSettingsService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _playerSettingsService = playerSettingsService;
        }
        public void Enter()
        {
            LoadProgressOrUnitNew();
            LoadSettings();
            _gameStateMachine.Enter<MainMenuState>();
        }

        public void Exit()
        {
        }

        private void LoadSettings()
        {
            _playerSettingsService.PlayerSettings = _saveLoadService.LoadGameSettings() ?? new PlayerSettings();
        }

        private void LoadProgressOrUnitNew() =>
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();

        private PlayerProgress NewProgress()
        {
            var playerProgress = new PlayerProgress(InitialLevel);
            return playerProgress;
        }
    }
    
    
    
}