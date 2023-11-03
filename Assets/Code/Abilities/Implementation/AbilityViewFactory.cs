using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Views;
using Code.Configs;
using ObjectPool.Contracts;
using UnityEngine;

namespace Code.Abilities.Implementation
{
    public class AbilityViewFactory : IAbilityViewFactory
    {
        private readonly IObjectPool _objectPool;
        private readonly AbilityNodeView _prefab;
        
        public AbilityViewFactory(MainConfig mainConfig, IObjectPool objectPool)
        {
            _objectPool = objectPool;
            _prefab = mainConfig.AbilityNodeViewPrefab;
        }
        
        public AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel, RectTransform parent)
        {
            var prefab = _objectPool.Spawn<AbilityNodeView>(_prefab.gameObject, parent);

            prefab.transform.localPosition = abilityModel.Position;
            
            prefab.UpdateView(abilityModel);

            return prefab;
        }
    }
}