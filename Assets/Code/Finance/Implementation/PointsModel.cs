using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Models;
using Code.Saves.Contracts;
using UnityEngine;

namespace Code.Finance.Implementation
{
    public class PointsModel : IPointsModel, ISaveble
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
            Save();
        }

        public void Load()
        {
            _points = PlayerPrefs.GetInt("Points");
            OnPointsChanger?.Invoke(_points);
        }
        
        private void Save()
        {
            PlayerPrefs.SetInt("Points", _points);
        }
    }
}