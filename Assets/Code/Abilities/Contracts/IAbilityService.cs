using Code.Abilities.Models;

namespace Code.Abilities.Contracts
{
    public interface IAbilityService
    {
        public AbilityModel[] AbilityModels { get; }
        public bool CanOpen(AbilityModel abilityModel);
        public bool CanClose(int index, AbilityModel[] abilityModels);
        public void OpenAbility(int index);
        public void CloseAbility(int index);
        public AbilityModel[] GetModels();
    }
}