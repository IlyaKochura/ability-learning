using Code.Abilities.Contracts;
using Code.Configs;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;

namespace Code.Ui.Screens
{
    public class AbilityScreen : IUIScreen
    {
        // private struct ModelViewPair
        // {
        //     public AbilityNodeView View { get; set; }
        //     public AbilityModel Model { get; set; }
        // }

        private readonly AbilityView _view;
        private readonly IAbilityViewFactory _abilityViewFactory;
        private readonly MainConfig _mainConfig;

        //private Dictionary<int, ModelViewPair> _dictionaryModelViewPairs = new();

        public AbilityScreen(AbilityView view, IScreenManager screenManager, IAbilityViewFactory abilityViewFactory, MainConfig mainConfig)
        {
            _view = view;
            _abilityViewFactory = abilityViewFactory;
            _mainConfig = mainConfig;

            _view.CloseClick += screenManager.GoBack;
        }
        
        public void Show()
        {
            // var abilityModel = _mainConfig.AbilityDefinitions.Select(definition => definition.GetModel());
            //
            // foreach (var model in abilityModel)
            // {
            //     var modelViewPair = new ModelViewPair();
            //     
            //     var view = _abilityViewFactory.CreateAbilityNodeView(model);
            //
            //     modelViewPair.View = view;
            //     modelViewPair.Model = model;
            //     
            //     _dictionaryModelViewPairs.Add(model.Index, modelViewPair);
            // }
            
            _view.Show();
        }

        public void Hide()
        {
            // foreach (var modelViewPair in _dictionaryModelViewPairs.Values)
            // {
            //     Object.Destroy(modelViewPair.View.gameObject);
            // }
            //
            // _dictionaryModelViewPairs.Clear();

            _view.Hide();
        }
    }
}