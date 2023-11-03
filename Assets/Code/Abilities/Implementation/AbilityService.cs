using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Code.Abilities.Contracts;
using Code.Abilities.Models;
using Code.Configs;
using Code.Finance.Contracts;
using Code.Saves.Contracts;
using ModestTree;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Abilities.Implementation
{
    public class AbilityService : IAbilityService, ISaveble
    {
        private class AbilitySaveModel
        {
            [DataMember] public Dictionary<int, bool> AbilityModels { get; private set; } = new();

            public void SetModels(AbilityModel[] abilityModels)
            {
                AbilityModels.Clear();

                foreach (var abilityModel in abilityModels)
                {
                    AbilityModels.Add(abilityModel.Index, abilityModel.IsOpen);
                }
            }
        }

        private HashSet<AbilityModel> _visitedModels = new();

        private readonly MainConfig _mainConfig;
        private readonly IPointsModel _pointsModel;
        private readonly AbilityModel _baseAbility;

        private AbilityModel _currentAbilityModel;
        private AbilityModel[] _abilityModels;
        private readonly AbilitySaveModel _saveModel;

        public AbilityModel[] AbilityModels => _abilityModels;
        public AbilityModel CurrentAbilityModel => _currentAbilityModel;

        public AbilityService(MainConfig mainConfig, IPointsModel pointsModel)
        {
            _mainConfig = mainConfig;
            _pointsModel = pointsModel;

            _abilityModels = GetModels();

            _baseAbility = _abilityModels.First(x => x.Index == mainConfig.BaseAbilityIndex);

            _currentAbilityModel = _baseAbility;

            _baseAbility.SetOpen(true);

            _saveModel = new AbilitySaveModel();
            _saveModel.SetModels(_abilityModels);
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
            _currentAbilityModel.SetOpen(true);
            Save();
        }

        public void CloseCurrentAbility()
        {
            _pointsModel.Add(_currentAbilityModel.Price);

            _currentAbilityModel.SetOpen(false);
            Save();
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

            return canOpen && _pointsModel.EnoughPoints(_currentAbilityModel.Price);
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

        public void Save()
        {
            _saveModel.SetModels(_abilityModels);
            var save = JsonConvert.SerializeObject(_saveModel);
            PlayerPrefs.SetString("AbilitySave", save);
        }

        public void Load()
        {
            var saveString = PlayerPrefs.GetString("AbilitySave");

            if (saveString.IsEmpty() || saveString == null)
            {
                return;
            }

            var abilitiesSave = JsonConvert.DeserializeObject<AbilitySaveModel>(saveString);

            if (abilitiesSave == null)
            {
                return;
            }

            foreach (var abilitySave in abilitiesSave.AbilityModels)
            {
                _abilityModels.First(model => model.Index == abilitySave.Key).SetOpen(abilitySave.Value);
            }
        }
    }
}