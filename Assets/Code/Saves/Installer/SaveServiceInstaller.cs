using Code.Saves.Implementation;
using Zenject;

namespace Code.Saves.Installer
{
    public class SaveServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SaveService>().AsSingle();
        }
    }
}