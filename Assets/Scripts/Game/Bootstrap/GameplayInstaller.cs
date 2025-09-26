using System.Net.Mime;
using Cinemachine;
using Game.CameraLogic;
using Game.Infrastructure.AssetsManagement;
using Game.Infrastructure.Factory;
using Game.Infrastructure.Services;
using Game.Infrastructure.Services.Input;
using Game.Infrastructure.Services.SaveLoad;
using Game.Infrastructure.Signals;
using Game.Infrastructure.States;
using SimpleInputNamespace;
using UnityEngine;
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
            BindSignals();

        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<PlayerDiedSignal>();
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