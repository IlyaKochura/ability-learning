using Code.Abilities.Models;
using UnityEngine;

namespace Code.Abilities.Contracts
{
    public interface ILinkDrawer
    {
        public void DrawLink(AbilityModel model, RectTransform parent);
    }
}