using Code.Saves.Contracts;
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

        public override void Start()
        {
            var saveLoaders = Container.ResolveAll<ILoadSavesService>();

            foreach (var saveLoader in saveLoaders)
            {
                saveLoader.Load();
            }
        }
    }
}