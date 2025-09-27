using System.Collections.Generic;
using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.Services.Input;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Player;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly DiContainer _container;
        private readonly IProgressWatchersRegister _progressWatchersRegister;


        public GameFactory(IAssets  assets, DiContainer container, IProgressWatchersRegister  progressWatchersRegister)
        {
            _assets = assets;
            _container = container;
            _progressWatchersRegister = progressWatchersRegister;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            GameObject playerPref = _assets.Load(AssetsPath.Player);
            GameObject player = _container.InstantiatePrefab(playerPref,at, Quaternion.identity, null);
            
            RegisterProgressWatchers(player);
            return player;
        }

        private void RegisterProgressWatchers(GameObject player)
        {
            foreach (ISaveProgressReader progressReader in player.GetComponentsInChildren<ISaveProgressReader>())
            {
                _progressWatchersRegister.Register(progressReader);

            }
        }

    }
}