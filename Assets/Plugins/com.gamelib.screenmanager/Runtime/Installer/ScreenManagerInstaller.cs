using Zenject;

namespace ScreenManager.Runtime.Implementation
{
    public class ScreenManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ScreenManager>().AsSingle();
        }
    }
}