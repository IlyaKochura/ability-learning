using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Configs;
using Code.Finance.Contracts;
namespace Code.Abilities.Implementation
{
    public class AbilityService : IAbilityService
    {
        private readonly MainConfig _mainConfig;
        private readonly IPointsModel _pointsModel;
        private readonly AbilityModel _baseAbility;

        public AbilityService(MainConfig mainConfig, IPointsModel pointsModel)
        {
            _mainConfig = mainConfig;
            _pointsModel = pointsModel;
        }

        public bool CanClose(int index, AbilityModel[] abilityModels)
        {
            throw new System.NotImplementedException();
        }

        public AbilityModel[] GetModels()
        {
            var abilityDefinitions = _mainConfig.AbilityDefinitions;

            return abilityDefinitions.Select(definition => definition.GetModel()).ToArray();
        }
        
        public bool CanOpen(int index, AbilityModel[] abilityModels)
        {
            var ability = abilityModels.FirstOrDefault(abilityModel => abilityModel.Index == index);

            if (ability == default)
            {
                return false;
            }

            return _pointsModel.TrySpentPoints(ability.Price);
        }
        
        

    }
}