using System;
using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Save;
using Code.Saves.Contracts;

namespace Code.Finance.Implementation
{
    internal class PointsService : IPointsService, ILoadSavesService
    {
        private readonly ISaveService _saveService;
        private readonly MainConfig _mainConfig;
        private readonly PointsSaveModel _pointsSaveModel;

        private int _points;
        public event Action<int> OnPointsChanged;
        public PointsService(ISaveService saveService)
        {
            _saveService = saveService;

            _pointsSaveModel = new PointsSaveModel();
        }

        public void Add(int count)
        {
            _points += count;
            Save();
            OnPointsChanged?.Invoke(_points);
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
            Save();
            OnPointsChanged?.Invoke(_points);
        }

        public void Load()
        {
            var pointsSaveModel = _saveService.GetSave<PointsSaveModel>();

            if (pointsSaveModel != null)
            {
                _points = pointsSaveModel.Points;
            }
            
            OnPointsChanged?.Invoke(_points);
        }

        private void Save()
        {
            _pointsSaveModel.SetPoints(_points);
            _saveService.Save(_pointsSaveModel);
        }
    }
}