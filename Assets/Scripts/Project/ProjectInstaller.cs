using Game.Infrastructure;
using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.Coroutine;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Scene;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.States;
using Game.Infrastructure.States.StateMachine;
using UnityEngine;
using Zenject;

namespace Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Bind<StatesFactory>().AsSingle();
            Container.Bind<IPersistentProgressService>().To<PersistentProgressService>().AsSingle();
            Container.Bind<SceneLoader>().AsSingle();

            
            Container.Bind<ICoroutineRunner>()
                .To<CoroutineRunner>()
                .FromComponentOn(gameObject)
                .AsSingle();
            
            Container.Bind<IProgressWatchersRegister>().To<ProgressWatchersRegister>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            
            Container.Bind<ProjectBootstrapper>().FromComponentOnRoot().AsSingle();
    

        }
        

        
    }
}
