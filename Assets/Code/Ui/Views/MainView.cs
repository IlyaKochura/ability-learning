using System;
using ScreenManager.Runtime.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Ui.Views
{
    public class MainView : BaseView
    {
        [SerializeField] private Button _openAbilityButton;
        [SerializeField] private Button _earnPointsButton;
        [SerializeField] private TMP_Text _pointsCount;

        public Action OpenAbilityClick { get; set; }
        public Action EarnPointsClick { get; set; }

        private void Start()
        {
            _openAbilityButton.onClick.AddListener(() => OpenAbilityClick?.Invoke());
            _earnPointsButton.onClick.AddListener(() => EarnPointsClick?.Invoke());
        }

        public void UpdatePoints(string countPoints)
        {
            _pointsCount.SetText(countPoints);
        }
    }
}