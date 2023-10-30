using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;

namespace Code.Ui.Screens
{
    public class AbilityScreen : IUIScreen
    {
        private readonly AbilityView _view;

        public AbilityScreen(AbilityView view, IScreenManager screenManager)
        {
            _view = view;

            _view.CloseClick += screenManager.GoBack;
        }
        
        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}