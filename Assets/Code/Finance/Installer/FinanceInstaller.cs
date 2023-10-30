using Code.Finance.Contracts;
using Code.Finance.Implementation;
using Zenject;

namespace Code.Finance.Installer
{
    public class FinanceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointsModel>().AsSingle();
        }

        public override void Start()
        {
            var model = Container.Resolve<IPointModel>();
            model.Load();
        }
    }
}