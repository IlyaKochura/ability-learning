using System;
using Code.Abilities.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Abilities.Views
{
    public class AbilityNodeView : MonoBehaviour
    {
        [SerializeField] private Image _mainImage;
        [SerializeField] private Image _sideImage;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _button;

        public Action CLickOnNode { get; set; }
        
        public void Start()
        {
            _button.onClick.AddListener(() => CLickOnNode?.Invoke());
        }

        public void SetAction(Action action)
        {
            _button.onClick.AddListener(() => action?.Invoke());
        }
        
        public void UpdateView(AbilityModel model)
        {
            _mainImage.color = model.MainNodeColor;
            _sideImage.color = model.IsOpen ? model.OpenColor : model.ClosedColor;
            _titleText.SetText(model.Title);
            _descriptionText.SetText(model.Description);
            _priceText.SetText($"{model.Price}");
        }
    }
}