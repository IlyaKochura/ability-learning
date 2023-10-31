using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Views;
using Code.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Abilities.Implementation
{
    public class AbilityViewFactory : IAbilityViewFactory
    {
        private readonly AbilityNodeView _prefab;
        
        public AbilityViewFactory(MainConfig mainConfig)
        {
            _prefab = mainConfig.AbilityNodeViewPrefab;
        }
        
        public AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel, RectTransform parent)
        {
            var prefab = Object.Instantiate(_prefab, parent);

            prefab.transform.localPosition = abilityModel.Position;
            
            prefab.UpdateView(abilityModel);

            return prefab;
        }
    }
}