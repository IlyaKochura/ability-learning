using Code.Saves.Contracts;
using Code.Ui.Screens;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;
using UnityEngine;
using Zenject;

namespace Code.Ui.InstallerUI
{
    public class InstallerUi : MonoInstaller
    {
        [SerializeField] private MainView _mainView;
        [SerializeField] private AbilityView _abilityView;

        public override void InstallBindings()
        {
            InstallViews();
            InstallScreens();
        }

        private void InstallScreens()
        {
            Container.BindInterfacesTo<MainScreen>().AsSingle();
            Container.BindInterfacesTo<AbilityScreen>().AsSingle();
        }

        private void InstallViews()
        {
            Container.Bind<MainView>().FromInstance(_mainView).AsSingle();
            Container.Bind<AbilityView>().FromInstance(_abilityView).AsSingle();
        }

        public override void Start()
        {
            ResolveScreenManager();

            var saves = Container.ResolveAll<ISaveble>();

            foreach (var save in saves)
            {
                save.Load();
            }
        }

        private void ResolveScreenManager()
        {
            var screenManager = Container.Resolve<IScreenManager>();
            screenManager.Resolve(Container);
            
            screenManager.ShowScreen<MainScreen>();
        }
    }
}