using System.Runtime.Serialization;
using Code.Saves.Contracts;

namespace Code.Finance.Save
{
    public class PointsSaveModel : ISaveModel
    {
        [DataMember] public int Points { get; set; }
        
        public void SetPoints(int points)
        {
            Points = points;
        }
    }
}