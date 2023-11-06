using UnityEngine;

namespace Code.Abilities.Models
{
    public class AbilityModel
    {
        public int Index { get; set; }
        public int Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Color MainNodeColor { get; set; }
        public Color OpenColor { get; set; }
        public Color ClosedColor { get; set; }
        public int[] LinkedIndexes { get; set; }
        public bool IsOpen { get; private set; }
        public Vector2 Position { get; set; }

        public AbilityModel(bool open = false)
        {
            IsOpen = open;
        }

        public void SetOpen(bool open)
        {
            IsOpen = open;
        }
        
        public override string ToString()
        {
            return $"Ability {Index} is open: {IsOpen}";
        }
    }
}