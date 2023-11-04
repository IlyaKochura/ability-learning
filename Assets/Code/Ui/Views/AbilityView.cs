using System;
using ScreenManager.Runtime.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Ui.Views
{
    public class AbilityView : BaseView
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeAllButton;
        [SerializeField] private RectTransform _abilityPlaceholder;

        public RectTransform AbilityPlaceHolder => _abilityPlaceholder;
        
        public Action ExitClick { get; set; }
        public Action CloseClick { get; set; }
        public Action OpenClick { get; set; }
        public Action CloseAllClick { get; set; }

        private void Start()
        {
            _exitButton.onClick.AddListener(() => ExitClick?.Invoke());
            _closeButton.onClick.AddListener(() => CloseClick?.Invoke());
            _openButton.onClick.AddListener(() => OpenClick?.Invoke());
            _closeAllButton.onClick.AddListener(() => CloseAllClick?.Invoke());
        }

        public void SetInteractableOpenButton(bool interactable)
        {
            _openButton.interactable = interactable;
        }
        
        public void SetInteractableCloseButton(bool interactable)
        {
            _closeButton.interactable = interactable;
        }
        
        public void SetInteractableCloseAllButton(bool interactable)
        {
            _closeAllButton.interactable = interactable;
        }
    }
}