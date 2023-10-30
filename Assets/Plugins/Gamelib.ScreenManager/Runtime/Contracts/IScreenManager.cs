using Zenject;

namespace ScreenManager.Runtime.Contracts
{
    public interface IScreenManager
    {
        void ShowScreen<T>() where T : IUIScreen;
        void HideScreen<T>() where T : IUIScreen;
        void GoBack();
        void Resolve(DiContainer diContainer);
    }
}
