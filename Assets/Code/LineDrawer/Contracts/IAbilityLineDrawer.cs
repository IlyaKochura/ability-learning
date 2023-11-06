using Code.Abilities.Models;
using UnityEngine;

namespace Code.LineDrawer.Contracts
{
    public interface IAbilityLineDrawer
    {
        public void DrawLineForAbility(AbilityModel abilityModel, RectTransform parent);
    }
}