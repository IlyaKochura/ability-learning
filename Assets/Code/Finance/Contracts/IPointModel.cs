using UnityEngine;

namespace Code.Finance.Contracts
{
    public interface IPointModel
    {
        public delegate void OnPointsChanged(int pointsCount);
        
        public event OnPointsChanged OnPointsChanger;
        public void Add(int count);
        public void Load();
    }
}