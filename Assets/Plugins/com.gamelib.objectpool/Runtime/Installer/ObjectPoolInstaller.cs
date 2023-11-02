using Zenject;

namespace ObjectPool.Runtime.Installer
{
    public class ObjectPoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Pool.Runtime.ObjectPool>().AsSingle();
        }
    }
}