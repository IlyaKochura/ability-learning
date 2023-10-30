using Code.Finance.Contracts;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;

namespace Code.Ui.Screens
{
    public class MainScreen : IUIScreen
    {
        private readonly MainView _view;
        private readonly IScreenManager _screenManager;
        private readonly IPointModel _pointModel;

        public MainScreen(MainView view, IScreenManager screenManager, IPointModel pointModel)
        {
            _view = view;
            _screenManager = screenManager;
            _pointModel = pointModel;

            _view.OpenAbilityClick += ShowAbilityWindow;
            _view.EarnPointsClick += EarnPoints;

            _pointModel.OnPointsChanger += UpdatePoints;
        }

        private void ShowAbilityWindow()
        {
            _screenManager.ShowScreen<AbilityScreen>();
        }

        public void Show()
        {
            _view.Show();
        }

        private void UpdatePoints(int restorePoints)
        {
            _view.UpdatePoints($"Points {restorePoints}");
        }
        private void EarnPoints()
        {
            _pointModel.Add(10);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}