using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Views;
using Code.Configs;
using Code.LineDrawer.Contracts;
using ObjectPool.Contracts;
using UnityEngine;

namespace Code.Abilities.Implementation
{
    internal class AbilityViewFactory : IAbilityViewFactory
    {
        private readonly IObjectPool _objectPool;
        private readonly IAbilityLineDrawer _abilityLineDrawer;

        private readonly AbilityNodeView _prefab;

        public AbilityViewFactory(MainConfig mainConfig, IObjectPool objectPool, IAbilityLineDrawer abilityLineDrawer)
        {
            _objectPool = objectPool;
            _abilityLineDrawer = abilityLineDrawer;
            _prefab = mainConfig.AbilityNodeViewPrefab;
        }
        
        public AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel, RectTransform abilityParent, RectTransform lineParent)
        {
            var prefab = _objectPool.Spawn<AbilityNodeView>(_prefab.gameObject, abilityParent);

            prefab.transform.localPosition = abilityModel.Position;
            
            prefab.UpdateView(abilityModel);
            
            _abilityLineDrawer.DrawLineForAbility(abilityModel, lineParent);

            return prefab;
        }
    }
}