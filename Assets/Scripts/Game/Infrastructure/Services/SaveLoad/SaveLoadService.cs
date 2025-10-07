using Data;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace Game.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IProgressWatchersRegister _register;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly IPlayerSettingsService _playerSettingsService;
        private const string ProgressKey = "Progress";
        private const string SettingKey = "Settings";

        public SaveLoadService(IProgressWatchersRegister register, IPersistentProgressService  persistentProgressService, IPlayerSettingsService playerSettingsService)
        {
            _register = register;
            _persistentProgressService = persistentProgressService;
            _playerSettingsService = playerSettingsService;
        }

        public PlayerProgress LoadProgress()
        {
            if (!ExtractData(out var json,ProgressKey)) return null;
            return json.ToDeserialized<PlayerProgress>();
        }


        public void SaveProgress()
        {
            foreach (var writer in _register.Writers)
            {
                writer.UpdateProgress(_persistentProgressService.Progress);
            }
            string json = _persistentProgressService.Progress.ToJson();
            PlayerPrefs.SetString(ProgressKey, json);
            PlayerPrefs.Save();

        }

        public PlayerSettings LoadGameSettings()
        {
            if (!ExtractData(out var json,SettingKey)) return null;
            return json.ToDeserialized<PlayerSettings>();
        }

        private static bool ExtractData(out string json, string key)
        {
            json = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(json))
            {
                return false;
            }

            return true;
        }

        public void SaveGameSettings()
        {
            PlayerPrefs.SetString(SettingKey,_playerSettingsService.PlayerSettings.ToJson());
            PlayerPrefs.Save();
        }
    }
}