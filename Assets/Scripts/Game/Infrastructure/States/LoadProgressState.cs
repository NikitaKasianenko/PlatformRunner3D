using Data;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;

namespace Game.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine gameStateMachine,IPersistentProgressService  progressService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }
        public void Enter()
        {
            LoadProgressOrUnitNew();
            _gameStateMachine.Enter<LoadLevelState,string>(_progressService.Progress.WorldData.PositionOnLevel.Level);
        }

        public void Exit()
        {
        }

        private void LoadProgressOrUnitNew() =>
            _progressService.Progress =
                _saveLoadService.LoadProgress()
                ?? NewProgress();

        private PlayerProgress NewProgress() => new PlayerProgress(initialLevel: "Game");
    }
    
    
    
}