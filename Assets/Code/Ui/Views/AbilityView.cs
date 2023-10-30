using System;
using ScreenManager.Runtime.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Ui.Views
{
    public class AbilityView : BaseView
    {
        [SerializeField] private Button _closeButton;
        
        public Action CloseClick { get; set; }

        private void Start()
        {
            _closeButton.onClick.AddListener(() => CloseClick?.Invoke());
        }
    }
}