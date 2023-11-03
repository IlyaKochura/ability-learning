using Code.Abilities.Implementation;
using Zenject;

namespace Code.Abilities.Installer
{
    public class AbilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LinkDrawer>().AsSingle();
            Container.BindInterfacesTo<AbilityViewFactory>().AsSingle();
            Container.BindInterfacesTo<AbilityService>().AsSingle().NonLazy();
        }
    }
}