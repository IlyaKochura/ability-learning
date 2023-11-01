using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Models;

namespace Code.Finance.Implementation
{
    public class PointsModel : IPointsModel
    {
        private readonly FinanceStorage _financeStorage;
        
        private int _points;

        public PointsModel(MainConfig mainConfig)
        {
            _financeStorage = mainConfig.FinanceStorage;
        }
        
        public event IPointsModel.OnPointsChanged OnPointsChanger;

        public void Add(int count)
        {
            _points += count;
            OnPointsChanger?.Invoke(_points);
            Save();
        }

        public bool EnoughPoints(int count)
        {
            return _points - count >= 0;
        }

        public void SpentPoints(int count)
        {
            if (_points - count < 0)
            {
                return;
            }
            
            _points -= count;
            OnPointsChanger?.Invoke(_points);
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