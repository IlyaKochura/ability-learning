using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Models;

namespace Code.Finance.Implementation
{
    public class PointsModel : IPointModel
    {
        private readonly FinanceStorage _financeStorage;
        
        private int _points;

        public PointsModel(MainConfig mainConfig)
        {
            _financeStorage = mainConfig.FinanceStorage;
        }

        public event IPointModel.OnPointsChanged OnPointsChanger;

        public void Add(int count)
        {
            _points += count;
            OnPointsChanger?.Invoke(_points);
            Save();
        }

        public bool TrySpentPoints(int count)
        {
            if (_points - count < 0)
            {
                return false;
            }
            
            _points -= count;
            OnPointsChanger?.Invoke(_points);
            return true;
        }

        public void Load()
        {
            _points = _financeStorage.PointsCount;
            OnPointsChanger?.Invoke(_points);
        }
        
        private void Save()
        {
            _financeStorage.SetPoints(_points);
        }
    }
}