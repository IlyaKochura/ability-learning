using Code.Configs;
using Code.Finance.Contracts;
using Code.Finance.Save;
using Code.Saves.Contracts;

namespace Code.Finance.Implementation
{
    public class PointsService : IPointsService
    {
        private readonly ISaveService _saveService;
        private readonly MainConfig _mainConfig;

        public PointsService(ISaveService saveService)
        {
            _saveService = saveService;

            _pointsSaveModel = new PointsSaveModel();
            
            Load();
        }

        private int _points;
        
        private readonly PointsSaveModel _pointsSaveModel;
        public event IPointsService.OnPointsChanged OnPointsChanger;

        public void Add(int count)
        {
            _points += count;
            Save();
            OnPointsChanger?.Invoke(_points);
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
            OnPointsChanger?.Invoke(_points);
        }

        public int GetPoints()
        {
            return _points;
        }

        private void Load()
        {
            var pointsSaveModel = _saveService.GetSave<PointsSaveModel>();
            
            if (pointsSaveModel == null) 
            {
                return;
            }

            _points = pointsSaveModel.Points;
        }
        
        private void Save()
        {
            _pointsSaveModel.SetPoints(_points);
            _saveService.Save(_pointsSaveModel);
        }
    }
}