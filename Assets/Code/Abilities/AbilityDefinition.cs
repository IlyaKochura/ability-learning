using System;
using Code.Abilities.Models;
using UnityEngine;

namespace Code.Abilities
{
    [Serializable, CreateAssetMenu(menuName = "Configs/Ability/AbilityDefinition")]
    public class AbilityDefinition : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private int _price;
        [SerializeField] private string _title;
        [SerializeField] private string _description;
        [SerializeField] private Color _mainNodeColor;
        [SerializeField] private Color _openColor;
        [SerializeField] private Color _closedColor;
        [SerializeField] private int[] _relationshipIndexes;
        [SerializeField] private Vector2 _position;

        public AbilityModel GetModel()
        {
            var model = new AbilityModel
            {
                Index = _index,
                Price = _price,
                Title = _title,
                Description = _description,
                MainNodeColor = _mainNodeColor,
                OpenColor = _openColor,
                ClosedColor = _closedColor,
                LinkedIndexes = _relationshipIndexes,
                Position = _position
            };

            return model;
        }
    }
}