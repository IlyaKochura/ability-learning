using System;
using Code.Abilities.Models;
using ObjectPool.Runtime.Contracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Abilities.Views
{
    public class AbilityNodeView : MonoBehaviour, IRecycle
    {
        [SerializeField] private Image _mainImage;
        [SerializeField] private Image _sideImage;
        [SerializeField] private GameObject _currentImage;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _button;
        
        public void SetAction(Action<int> action, int nodeIndex)
        {
            _button.onClick.AddListener(() => action?.Invoke(nodeIndex));
        }

        public void SetCurrent(bool current)
        {
            _currentImage.SetActive(current);
        }
        
        public void UpdateView(AbilityModel model)
        {
            _mainImage.color = model.MainNodeColor;
            _sideImage.color = model.IsOpen ? model.OpenColor : model.ClosedColor;
            _titleText.SetText(model.Title);
            _descriptionText.SetText(model.Description);
            _priceText.SetText($"{model.Price}");
        }

        public void Recycle()
        {
            gameObject.SetActive(false);
        }
    }
}