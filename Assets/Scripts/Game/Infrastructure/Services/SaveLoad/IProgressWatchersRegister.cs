using System.Collections.Generic;
using Game.Infrastructure.Services.PersistentProgress;

namespace Game.Infrastructure.Services.SaveLoad
{
    public interface IProgressWatchersRegister
    {
        void Register(ISaveProgressReader reader);
        IReadOnlyList<ISaveProgressReader> Readers { get; }
        IReadOnlyList<ISaveProgress> Writers { get; }
        void CleanUp();
    }
}