using Data;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Game.Scene.Checkpoint
{
    public abstract class CheckPointBase : MonoBehaviour , ISaveProgressReader
    {
        protected string _composeCollectedKey = "Key";
        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;

        [Inject]
        public void Construct(ISaveLoadService saveLoadService, IPersistentProgressService progressService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }

        protected void HandleSave()
        {
            if (!_progressService.Progress.WorldData.CheckPoints.Contains(_composeCollectedKey))
                _progressService.Progress.WorldData.CheckPoints.Add(_composeCollectedKey);

            _progressService.Progress.WorldData.SetPositionForLevel(GlobalUtils.CurrentLevel(), transform.position.AsVectorData());

            _saveLoadService.SaveProgress();
        }

        public virtual void DisableSelf()
        {
            if (gameObject != null) gameObject.SetActive(false);
        }
        
        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.WorldData.CheckPoints.Contains(_composeCollectedKey))
            {
                DisableSelf();
            }
        }
    }
}