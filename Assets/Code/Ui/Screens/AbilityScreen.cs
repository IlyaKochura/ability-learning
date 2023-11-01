using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Views;
using Code.Configs;
using Code.Ui.Views;
using ScreenManager.Runtime.Contracts;
using Object = UnityEngine.Object;

namespace Code.Ui.Screens
{
    public class AbilityScreen : IUIScreen
    {
        private readonly AbilityView _view;
        private readonly IAbilityViewFactory _abilityViewFactory;
        private readonly IAbilityService _abilityService;
        private readonly MainConfig _mainConfig;

        private Dictionary<int, AbilityNodeView> _dictionaryModelViewPairs = new();

        private AbilityNodeView _currentAbilityView;

        public AbilityScreen(AbilityView view, IScreenManager screenManager, IAbilityViewFactory abilityViewFactory,
            IAbilityService abilityService)
        {
            _view = view;
            _abilityViewFactory = abilityViewFactory;
            _abilityService = abilityService;

            _view.CloseClick += screenManager.GoBack;

            _view.OpenButton.onClick.AddListener(OpenAbility);
            _view.CloseButton.onClick.AddListener(CloseAbility);
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

                _dictionaryModelViewPairs.Add(model.Index, view);
            }
        }

        private void SetCurrentNode(int index)
        {
            _abilityService.SetCurrentAbilityModel(index);

            _currentAbilityView.SetCurrent(false);

            _currentAbilityView = GetAbilityNodeView(index);

            _currentAbilityView.SetCurrent(true);
            
            UpdateInteractableButtons();
        }

        private AbilityNodeView GetAbilityNodeView(int index)
        {
            var abilityNodeView = _dictionaryModelViewPairs.FirstOrDefault(nodeView => nodeView.Key == index).Value;

            return abilityNodeView;
        }

        private void UpdateInteractableButtons()
        {
            _view.CloseButton.interactable = _abilityService.CanCloseCurrentAbility();
            _view.OpenButton.interactable = _abilityService.CanOpenCurrentAbility();
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