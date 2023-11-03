using System.Collections.Generic;
using System.Runtime.Serialization;
using Code.Abilities.Models;
using Code.Saves.Contracts;

namespace Code.Abilities.Save
{
    public class AbilitySaveModel : ISaveModel
    {
        [DataMember] public Dictionary<int, bool> AbilityModels { get; set; } = new();
        public void SetModels(AbilityModel[] abilityModels)
        {   
            AbilityModels.Clear();
        
            foreach (var abilityModel in abilityModels)
            {
                AbilityModels.Add(abilityModel.Index, abilityModel.IsOpen);
            }
        }
    }
}