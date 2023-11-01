using Code.Abilities.Models;

namespace Code.Abilities.Contracts
{
    public interface IAbilityService
    {
        public AbilityModel[] AbilityModels { get; }
        public AbilityModel CurrentAbilityModel { get; }
        public void SetCurrentAbilityModel(int index);
        public bool CanOpenCurrentAbility();
        public bool CanCloseCurrentAbility();
        public void OpenCurrentAbility();
        public void CloseCurrentAbility();
        public AbilityModel[] GetModels();
    }
}