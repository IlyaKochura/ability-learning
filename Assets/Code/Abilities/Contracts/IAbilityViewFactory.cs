using Code.Abilities.Models;
using Code.Abilities.Views;

namespace Code.Abilities.Contracts
{
    public interface IAbilityViewFactory
    {
        AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel);
    }
}