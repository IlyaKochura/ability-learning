using System.Collections.Generic;
using Code.Abilities.Contracts;
using Code.Abilities.Extensions;
using Code.Abilities.Models;
using Code.Configs;
using Code.LineDrawer.Contracts;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace Code.LineDrawer.Implementation
{
    public class AbilityLineDrawer : IAbilityLineDrawer
    {
        private readonly IAbilityService _abilityService;
        
        private readonly UILineRenderer _linePrefab;

        private readonly HashSet<AbilityModel> _linedModels = new();

        public AbilityLineDrawer(MainConfig mainConfig, IAbilityService abilityService)
        {
            _abilityService = abilityService;
            _linePrefab = mainConfig.LinePrefab;
        }
        
        public void DrawLineForAbility(AbilityModel abilityModel, RectTransform parent)
        {
            if (_linedModels.Contains(abilityModel))
            {
                return;
            }
        
            _linedModels.Add(abilityModel);
            
            var linkedNodes = abilityModel.GetLinkedModels(_abilityService.AbilityModels);
        
            foreach (var linkedNode in linkedNodes)
            {
                if (_linedModels.Contains(linkedNode))
                {
                    continue;
                }
                
                var linePrefab = Object.Instantiate(_linePrefab, parent);
        
                linePrefab.Points[0] = abilityModel.Position;
                linePrefab.Points[1] = linkedNode.Position;
            }
        }
    }
}