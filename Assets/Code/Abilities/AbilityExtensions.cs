using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Models;

namespace Code.Abilities
{
    public static class AbilityExtensions
    {
        public static AbilityModel[] GetOpenedLinkedModels(this AbilityModel abilityModel,
            AbilityModel[] abilityModels)
        {
            var indexesRelationship = abilityModel.LinkedIndexes;
            var openedLinkedAbilities = new List<AbilityModel>();

            foreach (var index in indexesRelationship)
            {
                var linkedNode = abilityModels.FirstOrDefault(node => node.Index == index);

                if (linkedNode == default)
                {
                    return null;
                }

                if (linkedNode.IsOpen)
                {
                    openedLinkedAbilities.Add(linkedNode);
                }
            }

            return openedLinkedAbilities.ToArray();
        }
        
        public static AbilityModel[] GetAllLinkedModels(this AbilityModel abilityModel,
            AbilityModel[] abilityModels)
        {
            var indexesRelationship = abilityModel.LinkedIndexes;
            var openedLinkedAbilities = new List<AbilityModel>();

            foreach (var index in indexesRelationship)
            {
                var linkedNode = abilityModels.FirstOrDefault(node => node.Index == index);

                if (linkedNode == default)
                {
                    return null;
                }

                openedLinkedAbilities.Add(linkedNode);
                
            }

            return openedLinkedAbilities.ToArray();
        }
    }
}