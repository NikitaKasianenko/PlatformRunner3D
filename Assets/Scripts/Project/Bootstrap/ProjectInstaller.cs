using Game.Infrastructure.Coroutine;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Scene;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.States.StateMachine;
using Project.StaticData;
using UnityEngine;
using Zenject;

namespace Project.Bootstrap
{
    public class ProjectInstaller : MonoInstaller
    {
        
        [SerializeField] public StaticDataService _staticDataService;

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
            
            Container.Bind<IStaticDataService>().FromInstance(_staticDataService).AsSingle();

        }
        
        
    }
}
