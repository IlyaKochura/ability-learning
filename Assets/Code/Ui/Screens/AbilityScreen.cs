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
        // private class ModelViewPair
        // {
        //     public AbilityNodeView View { get; }
        //     public AbilityModel Model { get; }
        //
        //     public ModelViewPair(AbilityNodeView abilityNodeView, AbilityModel abilityModel)
        //     {
        //         View = abilityNodeView;
        //         Model = abilityModel;
        //     }
        //     
        //     public void UpdateView()
        //     {
        //         View.UpdateView(Model);
        //     }
        //
        //     public void SetActionClickOnNode(Action<int> action)
        //     {
        //         View.SetAction(action, Model.Index);
        //     }
        //
        //     public void SetCurrentNode(bool current)
        //     {
        //         View.SetCurrent(current);
        //     }
        // }

        private readonly AbilityView _view;
        private readonly IAbilityViewFactory _abilityViewFactory;
        private readonly IAbilityService _abilityService;
        private readonly MainConfig _mainConfig;

        private Dictionary<int, AbilityNodeView> _dictionaryModelViewPairs = new();
        
        private AbilityNodeView _currentViewNode;
        private AbilityModel _currentModelNode;

        public AbilityScreen(AbilityView view, IScreenManager screenManager, IAbilityViewFactory abilityViewFactory,
            IAbilityService abilityService)
        {
            _view = view;
            _abilityViewFactory = abilityViewFactory;
            _abilityService = abilityService;

            _view.CloseClick += screenManager.GoBack;
            _view.OpenButton.onClick.AddListener(OpenAbility);
            _view.OpenButton.onClick.AddListener(CloseAbility);
        }

        public void Show()
        {
            FillAbilities();

            _view.Show();
        }

        private void FillAbilities()
        {
            foreach (var model in _abilityService.AbilityModels)
            {
                var view = _abilityViewFactory.CreateAbilityNodeView(model, _view.AbilityPlaceHolder);

                view.SetAction(SetCurrentNode, model.Index);

                _dictionaryModelViewPairs.Add(model.Index, view);
            }
        }

        private void SetCurrentNode(int index)
        {
            var currentNode = _dictionaryModelViewPairs.FirstOrDefault(modelViewPair => modelViewPair.Key == index)
                .Value;

            if (_currentViewNode != null)
            {
                _currentViewNode.SetCurrent(false);
            }

            _currentViewNode = currentNode;
            _currentViewNode.SetCurrent(true);

            var currentModel = _abilityService.AbilityModels.FirstOrDefault(model => model.Index == index);

            if (currentNode == null)
            {
                return;
            }

            _currentModelNode = currentModel;
        }

        private void OpenAbility()
        {
            if (CalculateOpen())
            {
                _currentModelNode.Open();
                _currentViewNode.UpdateView(_currentModelNode);
            }
        }
        
        private void CloseAbility()
        {
            
        }

        private bool CalculateOpen()
        {
            return _abilityService.CanOpen(_currentModelNode);
        }

        // private void CalculateClose()
        // {
        //     var models = _dictionaryModelViewPairs.Values.Select(modelViewPair => modelViewPair.Model).ToArray();
        //
        //     if (_abilityService.CanClose(_currentViewNode.Model.Index, models))
        //     {
        //         _currentViewNode.Model.Close();
        //         _currentViewNode.UpdateView();
        //     }
        // }

        public void Hide()
        {
            foreach (var modelView in _dictionaryModelViewPairs.Values)
            {
                Object.Destroy(modelView.gameObject);
            }

            _dictionaryModelViewPairs.Clear();

            _view.Hide();
        }
    }
}