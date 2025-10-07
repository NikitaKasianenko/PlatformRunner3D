using UI.Elements;
using Zenject;

namespace UI.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelMenu>().FromComponentInHierarchy().AsSingle();
            Container.Bind<SensetivitySliders>().FromComponentInHierarchy().AsSingle();
        }
    }
}