using Code.Finance.Implementation;
using Zenject;

namespace Code.Finance.Installer
{
    public class FinanceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointsService>().AsSingle();
        }
    }
}