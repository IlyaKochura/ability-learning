using Code.Abilities.Models;

namespace Code.Abilities.Contracts
{
    public interface IAbilityService
    {
        public bool CanOpen(int index, AbilityModel[] abilityModels);
        public bool CanClose(int index, AbilityModel[] abilityModels);
        public AbilityModel[] GetModels();
    }
}