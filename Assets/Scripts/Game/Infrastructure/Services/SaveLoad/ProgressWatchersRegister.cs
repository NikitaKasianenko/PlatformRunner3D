using System.Collections.Generic;
using Game.Infrastructure.Services.PersistentProgress;

namespace Game.Infrastructure.Services.SaveLoad
{
    public class ProgressWatchersRegister : IProgressWatchersRegister
    {
        private readonly List<ISaveProgressReader> _readers = new();
        private readonly List<ISaveProgress> _writers = new();

        public IReadOnlyList<ISaveProgressReader> Readers => _readers;
        public IReadOnlyList<ISaveProgress> Writers => _writers;
        public void CleanUp()
        {
            _writers.Clear();
            _readers.Clear();
        }

        public void Register(ISaveProgressReader reader)
        {
            if (_readers.Contains(reader)) return;
            _readers.Add(reader);
            if (reader is ISaveProgress writer && !_writers.Contains(writer))
            {
                _writers.Add(writer);
            }
        }
    }
}