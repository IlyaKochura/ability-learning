using Code.Abilities.Models;
using Code.Abilities.Views;
using UnityEngine;

namespace Code.Abilities.Contracts
{
    public interface IAbilityViewFactory
    {
        AbilityNodeView CreateAbilityNodeView(AbilityModel abilityModel, RectTransform abilityParent, RectTransform lineParent);
    }
}