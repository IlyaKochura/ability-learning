using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Views;
using Code.Configs;
using Code.Finance.Contracts;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;
using UnityEngine;

namespace Code.Ui.Screens
{
    public class AbilityScreen : IUIScreen
    {
        private readonly AbilityView _view;
        private readonly IAbilityViewFactory _abilityViewFactory;
        private readonly IAbilityService _abilityService;
        private readonly MainConfig _mainConfig;

        private Dictionary<int, AbilityNodeView> _dictionaryIndexViewPairs = new();

        private AbilityNodeView _currentAbilityView;

        public AbilityScreen(AbilityView view, IScreenManager screenManager, IAbilityViewFactory abilityViewFactory,
            IAbilityService abilityService, IPointsService pointsService)
        {
            _view = view;
            _abilityViewFactory = abilityViewFactory;
            _abilityService = abilityService;

            pointsService.OnPointsChanged += _ => UpdateInteractableButtons();
            
            _view.ExitClick += screenManager.GoBack;

            _view.CloseClick += CloseAbility;
            _view.OpenClick += OpenAbility;
            _view.CloseAllClick += CloseAllAbility;
        }

        public void Show()
        {
            FillAbilities();
            
            _currentAbilityView = GetAbilityNodeView(_abilityService.CurrentAbilityModel.Index);
            
            SetCurrentNode(_abilityService.CurrentAbilityModel.Index);

            _view.Show();
        }

        private void FillAbilities()
        {
            foreach (var model in _abilityService.AbilityModels)
            {
                var view = _abilityViewFactory.CreateAbilityNodeView(model, _view.AbilityPlaceHolder);

                view.SetAction(SetCurrentNode, model.Index);

                _dictionaryIndexViewPairs.Add(model.Index, view);
            }
        }

        private void SetCurrentNode(int index)
        {
            _abilityService.SetCurrentAbilityModel(index);

            if (_currentAbilityView != null)
            {
                _currentAbilityView.SetCurrent(false);
            }

            _currentAbilityView = GetAbilityNodeView(index);

            _currentAbilityView.SetCurrent(true);
            
            UpdateInteractableButtons();
        }

        private AbilityNodeView GetAbilityNodeView(int index)
        {
            var abilityNodeView = _dictionaryIndexViewPairs.FirstOrDefault(nodeView => nodeView.Key == index).Value;

            if (abilityNodeView == default)
            {
                Debug.LogError($"Not found Node with id: {index}");
                
                return null;
            }

            return abilityNodeView;
        }

        private void UpdateInteractableButtons()
        {
            _view.SetInteractableCloseButton(_abilityService.CanCloseCurrentAbility());
            _view.SetInteractableOpenButton(_abilityService.CanOpenCurrentAbility());
            _view.SetInteractableCloseAllButton(_abilityService.CanCloseAllAbility());
        }
        
        private void CloseAllAbility()
        {
            _abilityService.CloseAllOpenedAbility();
            
            UpdateInteractableButtons();
            UpdateAllViews();
        }

        private void OpenAbility()
        {
            if (_abilityService.CanOpenCurrentAbility())
            {
                _abilityService.OpenCurrentAbility();
                _currentAbilityView.UpdateView(_abilityService.CurrentAbilityModel);
                
                UpdateInteractableButtons();
            }
        }
        
        private void CloseAbility()
        {
            if (_abilityService.CanCloseCurrentAbility())
            {
                _abilityService.CloseCurrentAbility();
                _currentAbilityView.UpdateView(_abilityService.CurrentAbilityModel);
                
                UpdateInteractableButtons();
            }
        }

        private void UpdateAllViews()
        {
            foreach (var indexViewPair in _dictionaryIndexViewPairs)
            {
                var model = _abilityService.AbilityModels.FirstOrDefault(model => model.Index == indexViewPair.Key);

                if (model == default)
                {
                    continue;
                }
                
                indexViewPair.Value.UpdateView(model);
            }
        }

        public void Hide()
        {
            foreach (var modelView in _dictionaryIndexViewPairs.Values)
            {
                modelView.Recycle();
            }

            _dictionaryIndexViewPairs.Clear();

            _view.Hide();
        }
    }
}