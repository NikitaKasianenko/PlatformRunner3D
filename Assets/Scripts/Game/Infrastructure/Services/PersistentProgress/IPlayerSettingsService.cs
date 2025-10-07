using Data;

namespace Game.Infrastructure.Services.PersistentProgress
{
    public interface IPlayerSettingsService
    {
        public PlayerSettings PlayerSettings { get; set; }
    }
}