using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Views;
using Code.Configs;
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
        
        public AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel)
        {
            var prefab = Object.Instantiate(_prefab);
            
            prefab.UpdateView(abilityModel);

            return prefab;
        }
    }
}