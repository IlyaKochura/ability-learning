using Code.Finance.Contracts;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;

namespace Code.Ui.Screens
{
    public class MainScreen : IUIScreen
    {
        private readonly MainView _view;
        private readonly IScreenManager _screenManager;
        private readonly IPointsService _pointsService;

        public MainScreen(MainView view, IScreenManager screenManager, IPointsService pointsService)
        {
            _view = view;
            _screenManager = screenManager;
            _pointsService = pointsService;

            _view.OpenAbilityClick += ShowAbilityWindow;
            _view.EarnPointsClick += EarnPoints;

            _pointsService.OnPointsChanger += UpdatePoints;
        }

        private void ShowAbilityWindow()
        {
            _screenManager.ShowScreen<AbilityScreen>();
        }

        public void Show()
        {
            _view.UpdatePoints($"Points {_pointsService.GetPoints()}");
            
            _view.Show();
        }

        private void UpdatePoints(int restorePoints)
        {
            _view.UpdatePoints($"Points {restorePoints}");
        }
        
        private void EarnPoints()
        {
            _pointsService.Add(10);
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}