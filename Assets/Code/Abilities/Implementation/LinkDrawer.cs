using System.Collections.Generic;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Configs;
using UnityEngine;

namespace Code.Abilities.Implementation
{
    public class LinkDrawer : ILinkDrawer
    {
        private Dictionary<int, int[]> _drawingLinks;

        private readonly IAbilityService _abilityService;
        private readonly LineRenderer _prefab;

        public LinkDrawer(IAbilityService abilityService, MainConfig mainConfig)
        {
            _abilityService = abilityService;
            _prefab = mainConfig.LinePrefab;
        }

        public void DrawLink(AbilityModel model, RectTransform parent)
        {
            var modelsToConnect = model.GetAllLinkedModels(_abilityService.AbilityModels);

            foreach (var modelToConnect in modelsToConnect)
            {
                var connectLine = Object.Instantiate(_prefab, parent);

                connectLine.positionCount = 2;
                connectLine.useWorldSpace = false;
                connectLine.SetPosition(0, model.Position);
                connectLine.SetPosition(1, modelToConnect.Position);
            }
        }
    }
}