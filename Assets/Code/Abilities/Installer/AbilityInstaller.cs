using Code.Abilities.Implementation;
using Code.LineDrawer.Implementation;
using Zenject;

namespace Code.Abilities.Installer
{
    public class AbilityInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AbilityLineDrawer>().AsSingle();
            Container.BindInterfacesTo<AbilityViewFactory>().AsSingle();
            Container.BindInterfacesTo<AbilityService>().AsSingle().NonLazy();
        }
    }
}