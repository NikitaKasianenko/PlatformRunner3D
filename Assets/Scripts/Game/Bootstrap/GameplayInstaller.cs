using Cinemachine;
using Game.CameraLogic;
using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Scene;
using Game.Infrastructure.Services.Input;
using Game.Infrastructure.Services.PersistentProgress;
using Game.Infrastructure.Signals;
using Game.Infrastructure.States;
using SimpleInputNamespace;
using Zenject;

namespace Game.Bootstrap
{
    public class GameplayInstaller : MonoInstaller
    {
        public Touchpad touchpad;
        public Joystick joystick;
        public CinemachineVirtualCamera virtualCamera;
        
        public override void InstallBindings()
        {
            BindStates();
            BindInput();
            BindCamera();
            BindGameSceneBootstrap();
            BindAssets();
            BindGameFactory();
            BindReaders();

        }

        private void BindReaders()
        {
            // Container.BindInterfacesTo<SceneReadersRegistrar>().AsSingle();
            
            Container.Bind<ISaveProgressReader>()
                .FromComponentsInHierarchy()
                .AsTransient();
        }

        private void BindGameSceneBootstrap()
        {
            Container.BindInterfacesTo<GameSceneBootstrap>().AsSingle();
        }



        private void BindStates()
        {
            Container.Bind<InitializeGameplayState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
        }

        private void BindInput()
        {
            Container.Bind<IInputService>()
                .To<MobileInputService>()
                .AsSingle()
                .WithArguments(touchpad, joystick);
        }

        private void BindCamera()
        {
            Container.BindInstance(virtualCamera).AsSingle();
            Container.BindInterfacesTo<CameraFollow>().AsSingle();
        }
        
        private void BindAssets()
        {
            Container.Bind<IAssets>().To<AssetsProvider>().AsSingle();
        }

        private void BindGameFactory()
        {
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
        }
    }
}