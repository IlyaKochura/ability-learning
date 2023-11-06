using System;

namespace Code.Finance.Contracts
{
    public interface IPointsService
    { 
        public Action<int> OnPointsChanged { get; set; }
        public void Add(int count);
        public bool EnoughPoints(int count);
        public void SpentPoints(int count);
    }
}