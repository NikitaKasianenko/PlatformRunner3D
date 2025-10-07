using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Scene
{
    public class SceneReadersRegistrar : IInitializable
    {
        readonly DiContainer _container;
        readonly IProgressWatchersRegister _watchersRegister;

        public SceneReadersRegistrar(DiContainer container, IProgressWatchersRegister watchersRegister)
        {
            _container = container;
            _watchersRegister = watchersRegister;
        }

        public void Initialize()
        {
            var readers = _container.ResolveAll<ISaveProgressReader>();

            foreach (var reader in readers)
            {
                if (reader is Object unityObj && unityObj == null)
                    continue;

                _watchersRegister.Register(reader);
            }
        }
    }
}