using UnityEngine;

namespace Code.Finance.Contracts
{
    public interface IPointsModel
    {
        public delegate void OnPointsChanged(int pointsCount);
        
        public event OnPointsChanged OnPointsChanger;
        public void Add(int count);
        public bool EnoughPoints(int count);
        public void SpentPoints(int count);
        public void Load();
    }
}