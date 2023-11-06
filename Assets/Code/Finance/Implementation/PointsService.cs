using System;
using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Save;
using Code.Saves.Contracts;

namespace Code.Finance.Implementation
{
    public class PointsService : IPointsService, ILoadSavesService
    {
        private readonly ISaveService _saveService;
        private readonly MainConfig _mainConfig;
        private readonly PointsSaveModel _pointsSaveModel;

        public PointsService(ISaveService saveService)
        {
            _saveService = saveService;

            _pointsSaveModel = new PointsSaveModel();
            
            Load();
        }

        private int _points;

        public Action<int> OnPointsChanged { get; set; }

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

            if (pointsSaveModel == null) 
            {
                OnPointsChanged?.Invoke(_points);
                return;
            }

            _points = pointsSaveModel.Points;
            OnPointsChanged?.Invoke(_points);
        }
        
        private void Save()
        {
            _pointsSaveModel.SetPoints(_points);
            _saveService.Save(_pointsSaveModel);
        }
    }
}