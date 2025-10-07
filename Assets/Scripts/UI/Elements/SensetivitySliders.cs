using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Elements
{
    public class SensetivitySliders : MonoBehaviour
    {
        [SerializeField] private Slider sensitivityXSlider;
        [SerializeField] private Slider sensitivityYSlider;
        private IPlayerSettingsService _playerSettingsService;
        private ISaveLoadService _saveLoadService;


        [Inject]
        public void Construct(IPlayerSettingsService playerSettingsService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _playerSettingsService = playerSettingsService;
        }
        
        private void OnEnable()
        {
            SetupListeners();
            SetupSliders();
        }

        private void SetupListeners()
        {
            sensitivityXSlider.onValueChanged.AddListener(OnSensitivityXChanged);
            sensitivityYSlider.onValueChanged.AddListener(OnSensitivityYChanged);
        }

        private void SetupSliders()
        {
            sensitivityXSlider.value = _playerSettingsService.PlayerSettings.Sensitivity.x;
            sensitivityYSlider.value = _playerSettingsService.PlayerSettings.Sensitivity.y;
        }

        private void OnSensitivityXChanged(float value)
        {
            _playerSettingsService.PlayerSettings.Sensitivity.x = value;
            _saveLoadService.SaveGameSettings();
        }

        private void OnSensitivityYChanged(float value)
        {
            _playerSettingsService.PlayerSettings.Sensitivity.y = value;
            _saveLoadService.SaveGameSettings();
        }

        private void OnDisable()
        {
            sensitivityXSlider.onValueChanged.RemoveListener(OnSensitivityXChanged);
            sensitivityYSlider.onValueChanged.RemoveListener(OnSensitivityYChanged);
            
        }
    }
}