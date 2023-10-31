using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Configs;
using UnityEngine;

namespace Code.Abilities.Implementation
{
    public class AbilityService : IAbilityService
    {
        public AbilityModel BaseAbility { get; }
        public AbilityModel[] Abilities { get; }
        
        public AbilityService(MainConfig mainConfig)
        {
            var abilityDefinitions = mainConfig.AbilityDefinitions;

            Abilities = abilityDefinitions.Select(definition => definition.GetModel()).ToArray();
            BaseAbility = Abilities.FirstOrDefault(model => model.Index == mainConfig.BaseAbilityIndex);
            
            if (BaseAbility == default)
            {
                Debug.LogError($"sdadsadadadasd");

                return;
            }
            
            BaseAbility.Open();
        }
        
    }
}