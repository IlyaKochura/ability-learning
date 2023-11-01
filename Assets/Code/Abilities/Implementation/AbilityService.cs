using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Configs;
using Code.Finance.Contracts;

namespace Code.Abilities.Implementation
{
    public class AbilityService : IAbilityService
    {
        private HashSet<AbilityModel> _visitedModels = new();

        private readonly MainConfig _mainConfig;
        private readonly IPointsModel _pointsModel;
        private readonly AbilityModel _baseAbility;

        private AbilityModel _currentAbilityModel;
        private AbilityModel[] _abilityModels;

        public AbilityModel[] AbilityModels => _abilityModels;
        public AbilityModel CurrentAbilityModel => _currentAbilityModel;

        public AbilityService(MainConfig mainConfig, IPointsModel pointsModel)
        {
            _mainConfig = mainConfig;
            _pointsModel = pointsModel;

            _abilityModels = GetModels();

            _baseAbility = _abilityModels.First(x => x.Index == mainConfig.BaseAbilityIndex);

            _currentAbilityModel = _baseAbility;
            
            _baseAbility.Open();
        }

        public void SetCurrentAbilityModel(int index)
        {
            _currentAbilityModel = GetAbilityModel(index);
        }

        public void OpenCurrentAbility()
        {
            if (!_pointsModel.EnoughPoints(_currentAbilityModel.Price))
            {
                return;
            }
            
            _pointsModel.SpentPoints(_currentAbilityModel.Price);
            _currentAbilityModel.Open();
        }

        public void CloseCurrentAbility()
        {
            _pointsModel.Add(_currentAbilityModel.Price);
            
            _currentAbilityModel.Close();
        }

        public AbilityModel[] GetModels()
        {
            var abilityDefinitions = _mainConfig.AbilityDefinitions;

            return abilityDefinitions.Select(definition => definition.GetModel()).ToArray();
        }

        private AbilityModel GetAbilityModel(int index)
        {
            var model = _abilityModels.FirstOrDefault(model => model.Index == index);

            return model;
        }

        public bool CanOpenCurrentAbility()
        {
            if (_currentAbilityModel.IsOpen)
            {
                return false;
            }
            
            var relationshipAbilities = _currentAbilityModel.GetOpenedAbilitiesRelationship(_abilityModels);

            if (relationshipAbilities == null)
            {
                return false;
            }

            var canOpen = false;
            
            foreach (var ability in relationshipAbilities)
            {
                if (!ability.IsOpen)
                {
                    continue;
                }

                canOpen = true;

            }

            return canOpen && _pointsModel.EnoughPoints(_currentAbilityModel.Price);
        }
        
        public bool CanCloseCurrentAbility()
        {
            if (_currentAbilityModel == null || !_currentAbilityModel.IsOpen || _currentAbilityModel == _baseAbility)
            {
                return false;
            }
            
            var relationshipOpenedAbilities = _currentAbilityModel.GetOpenedAbilitiesRelationship(_abilityModels);

            foreach (var relationshipOpenAbility in relationshipOpenedAbilities)
            {
                _visitedModels.Clear();

                if (CheckModel(_baseAbility, relationshipOpenAbility))
                {
                    continue;
                }
                
                return false;
            }
            
            return true;
        }

        private bool CheckModel(AbilityModel targetAbility, AbilityModel visitAbility)
        {
            if (targetAbility.Index == visitAbility.Index)
            {
                return true;
            }

            _visitedModels.Add(visitAbility);

            var visitAbilityRelationships = visitAbility.GetOpenedAbilitiesRelationship(_abilityModels);

            foreach (var ability in visitAbilityRelationships)
            {
                if (_visitedModels.Contains(ability))
                {
                    continue;
                }

                if (CheckModel(ability, targetAbility))
                {
                    return true;
                }
            }

            return false;
        }
    }
}