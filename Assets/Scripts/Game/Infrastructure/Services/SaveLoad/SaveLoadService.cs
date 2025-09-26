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
        private const string ProgressKey = "Progress";

        public SaveLoadService(IProgressWatchersRegister register, IPersistentProgressService  persistentProgressService)
        {
            _register = register;
            _persistentProgressService = persistentProgressService;
        }

        public PlayerProgress LoadProgress()
        {
            string json = PlayerPrefs.GetString(ProgressKey);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }

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
    }
}