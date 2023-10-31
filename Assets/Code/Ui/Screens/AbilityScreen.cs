using System;
using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Views;
using Code.Configs;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;
using Object = UnityEngine.Object;

namespace Code.Ui.Screens
{
    public class AbilityScreen : IUIScreen
    {
        private class ModelViewPair
        {
            public AbilityNodeView View { get; }
            public AbilityModel Model { get; }

            public ModelViewPair(AbilityNodeView abilityNodeView, AbilityModel abilityModel)
            {
                View = abilityNodeView;
                Model = abilityModel;
            }
            
            public void UpdateView()
            {
                View.UpdateView(Model);
            }

            public void SetActionClickOnNode(Action<int> action)
            {
                View.SetAction(action, Model.Index);
            }

            public void SetCurrentNode(bool current)
            {
                View.SetCurrent(current);
            }
        }

        private readonly AbilityView _view;
        private readonly IAbilityViewFactory _abilityViewFactory;
        private readonly IAbilityService _abilityService;
        private readonly MainConfig _mainConfig;

        private Dictionary<int, ModelViewPair> _dictionaryModelViewPairs = new();
        private ModelViewPair _currentNode;

        public AbilityScreen(AbilityView view, IScreenManager screenManager, IAbilityViewFactory abilityViewFactory,
            IAbilityService abilityService)
        {
            _view = view;
            _abilityViewFactory = abilityViewFactory;
            _abilityService = abilityService;

            _view.CloseClick += screenManager.GoBack;
        }

        public void Show()
        {
            FillAbilities();

            _view.Show();
        }

        private void FillAbilities()
        {
            foreach (var model in _abilityService.GetModels())
            {
                var view = _abilityViewFactory.CreateAbilityNodeView(model, _view.AbilityPlaceHolder);
                var modelViewPair = new ModelViewPair(view, model);

                modelViewPair.SetActionClickOnNode(SetCurrentNode);

                _dictionaryModelViewPairs.Add(model.Index, modelViewPair);
            }
        }

        private void SetCurrentNode(int index)
        {
            var currentNode = _dictionaryModelViewPairs.FirstOrDefault(modelViewPair => modelViewPair.Key == index);

            if (_currentNode != null)
            {
                _currentNode.SetCurrentNode(false);
            }

            _currentNode = currentNode.Value;
            _currentNode.SetCurrentNode(true);
        }

        public void Hide()
        {
            foreach (var modelViewPair in _dictionaryModelViewPairs.Values)
            {
                Object.Destroy(modelViewPair.View.gameObject);
            }

            _dictionaryModelViewPairs.Clear();

            _view.Hide();
        }
    }
}