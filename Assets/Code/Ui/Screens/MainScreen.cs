using Code.Finance.Contracts;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;

namespace Code.Ui.Screens
{
    public class MainScreen : IUIScreen
    {
        private readonly MainView _view;
        private readonly IScreenManager _screenManager;
        private readonly IPointsModel _pointsModel;

        public MainScreen(MainView view, IScreenManager screenManager, IPointsModel pointsModel)
        {
            _view = view;
            _screenManager = screenManager;
            _pointsModel = pointsModel;

            _view.OpenAbilityClick += ShowAbilityWindow;
            _view.EarnPointsClick += EarnPoints;

            _pointsModel.OnPointsChanger += UpdatePointses;
        }

        private void ShowAbilityWindow()
        {
            _screenManager.ShowScreen<AbilityScreen>();
        }

        public void Show()
        {
            _view.Show();
        }

        private void UpdatePointses(int restorePoints)
        {
            _view.UpdatePoints($"Points {restorePoints}");
        }
        
        private void EarnPoints()
        {
            _pointsModel.Add(10);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}