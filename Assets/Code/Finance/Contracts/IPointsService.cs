using System;

namespace Code.Finance.Contracts
{
    public interface IPointsService
    {
        public event Action<int> OnPointsChanged;
        public void Add(int count);
        public bool EnoughPoints(int count);
        public void SpentPoints(int count);
    }
}