using System.Collections.Generic;
using System.Linq;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Abilities.Save;
using Code.Configs;
using Code.Finance.Contracts;
using Code.Saves.Contracts;

namespace Code.Abilities.Implementation
{
    public class AbilityService : IAbilityService
    {
        private HashSet<AbilityModel> _visitedModels = new();

        private readonly MainConfig _mainConfig;
        private readonly IPointsService _pointsService;
        private readonly ISaveService _saveService;
        private readonly AbilityModel _baseAbility;

        private AbilityModel _currentAbilityModel;
        private AbilityModel[] _abilityModels;
        private readonly AbilitySaveModel _saveModel;

        public AbilityModel[] AbilityModels => _abilityModels;
        public AbilityModel CurrentAbilityModel => _currentAbilityModel;

        public AbilityService(MainConfig mainConfig, IPointsService pointsService, ISaveService saveService)
        {
            _mainConfig = mainConfig;
            _pointsService = pointsService;
            _saveService = saveService;

            _abilityModels = GetModels();

            _baseAbility = _abilityModels.First(x => x.Index == mainConfig.BaseAbilityIndex);

            _currentAbilityModel = _baseAbility;

            _baseAbility.SetOpen(true);

            _saveModel = new AbilitySaveModel();
           
            Load();
        }

        public void SetCurrentAbilityModel(int index)
        {
            _currentAbilityModel = GetAbilityModel(index);
        }

        public void OpenCurrentAbility()
        {
            if (!_pointsService.EnoughPoints(_currentAbilityModel.Price))
            {
                return;
            }

            _pointsService.SpentPoints(_currentAbilityModel.Price);
            _currentAbilityModel.SetOpen(true);
            Save();
        }

        public void CloseCurrentAbility()
        {
            _pointsService.Add(_currentAbilityModel.Price);

            _currentAbilityModel.SetOpen(false);
            Save();
        }

        private AbilityModel[] GetModels()
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

            var relationshipAbilities = _currentAbilityModel.GetOpenedLinkedModels(_abilityModels);

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

            return canOpen && _pointsService.EnoughPoints(_currentAbilityModel.Price);
        }

        public bool CanCloseAllAbility()
        {
            foreach (var model in _abilityModels)
            {
                if (model.Index == _baseAbility.Index)
                {
                    continue;
                }

                if (model.IsOpen)
                {
                    return true;
                }
            }

            return false;
        }

        public void CloseAllOpenedAbility()
        {
            foreach (var abilityModel in _abilityModels.GetAllOpenedModels())
            {
                if (abilityModel.Index != _mainConfig.BaseAbilityIndex)
                {
                    abilityModel.SetOpen(false);
                    _pointsService.Add(abilityModel.Price);
                }
            }
        }

        public bool CanCloseCurrentAbility()
        {
            if (_currentAbilityModel == null || !_currentAbilityModel.IsOpen || _currentAbilityModel == _baseAbility)
            {
                return false;
            }

            var linkedOpenedModels = _currentAbilityModel.GetOpenedLinkedModels(_abilityModels);

            foreach (var linkedOpenedModel in linkedOpenedModels)
            {
                _visitedModels.Clear();

                if (!CheckModels(_baseAbility, linkedOpenedModel, _currentAbilityModel))
                {
                    return false;
                }
            }

            return true;
        }

        private bool CheckModels(AbilityModel needVisitAbility, AbilityModel targetAbility, AbilityModel currentAbility)
        {
            if (needVisitAbility == targetAbility)
            {
                return true;
            }

            if (needVisitAbility == currentAbility)
            {
                return false;
            }

            _visitedModels.Add(needVisitAbility);

            var linkedAbilities = needVisitAbility.GetOpenedLinkedModels(_abilityModels);

            foreach (var linkedModel in linkedAbilities)
            {
                if (!_visitedModels.Contains(linkedModel))
                {
                    if (CheckModels(linkedModel, targetAbility, currentAbility))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private void Save()
        {
            _saveModel.SetModels(_abilityModels);
            _saveService.Save(_saveModel);
        }
        
        private void Load()
        {
            var saveModel = _saveService.GetSave<AbilitySaveModel>();

            if (saveModel == null)
            {
                return;
            }
        
            foreach (var abilitySave in saveModel.AbilityModels)
            {
                _abilityModels.First(model => model.Index == abilitySave.Key).SetOpen(abilitySave.Value);
            }
        }
    }
}