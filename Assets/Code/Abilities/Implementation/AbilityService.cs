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
        
        public AbilityModel[] AbilityModels { get; set; }

        public AbilityService(MainConfig mainConfig, IPointsModel pointsModel)
        {
            _mainConfig = mainConfig;
            _pointsModel = pointsModel;

            AbilityModels = GetModels();
        }

        public bool CanClose(int index, AbilityModel[] abilityModels)
        {
            var currentModel = abilityModels.FirstOrDefault(model => model.Index == index);

            if (currentModel == default)
            {
                return false;
            }
            
            if (!currentModel.IsOpen)
            {
                return false;
            }

            return true;
        }

        public void OpenAbility(int index)
        {
            
        }

        public void CloseAbility(int index)
        {
          
        }

        public AbilityModel[] GetModels()
        {
            var abilityDefinitions = _mainConfig.AbilityDefinitions;

            return abilityDefinitions.Select(definition => definition.GetModel()).ToArray();
        }
        
        public bool CanOpen(AbilityModel abilityModel)
        {
            var abilities = abilityModel.GetOpenedAbilitiesRelationship(GetModels());

            if (abilities == null)
            {
                return false;
            }

            var canOpen = false;
            
            foreach (var ability in abilities)
            {
                if (!ability.IsOpen)
                {
                    continue;
                }

                canOpen = true;

            }

            return canOpen && _pointsModel.TrySpentPoints(abilityModel.Price);

        }
    }
}