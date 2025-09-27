using Data;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Player
{
    public class CheckPoint : MonoBehaviour, ISaveProgressReader
    {
        [SerializeField] private string persistentId = "0";
        private string _composeCollectedKey = "Key";

        private ISaveLoadService _saveLoadService;
        private IPersistentProgressService _progressService;
        
        
        [Inject]
        public void Construct(ISaveLoadService saveLoadService, IPersistentProgressService progressService)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }
        
        private void Awake()
        {
            _composeCollectedKey = $"{gameObject.scene.name}:{persistentId}";
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.Player))
                return;

            if (!_progressService.Progress.WorldData.CheckPoints.Contains(_composeCollectedKey))
            {
                _progressService.Progress.WorldData.CheckPoints.Add(_composeCollectedKey);
            }

            _saveLoadService.SaveProgress();
            DisableSelf();
        }

        private void DisableSelf()
        {   
            gameObject.SetActive(false);
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