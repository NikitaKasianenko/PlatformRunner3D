using Data;

namespace Game.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        PlayerProgress LoadProgress();
        void SaveProgress();
        PlayerSettings LoadGameSettings();
        void SaveGameSettings();

    }
}