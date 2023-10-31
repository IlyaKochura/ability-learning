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
        [SerializeField] private RectTransform _abilityPlaceholder;

        public Button CloseButton => _closeButton;
        public Button OpenButton => _openButton;

        public RectTransform AbilityPlaceHolder => _abilityPlaceholder;
        
        public Action CloseClick { get; set; }

        private void Start()
        {
            _exitButton.onClick.AddListener(() => CloseClick?.Invoke());
        }
    }
}