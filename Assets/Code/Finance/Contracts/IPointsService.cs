namespace Code.Finance.Contracts
{
    public interface IPointsService
    {
        public delegate void OnPointsChanged(int pointsCount);
        public event OnPointsChanged OnPointsChanger;
        public void Add(int count);
        public bool EnoughPoints(int count);
        public void SpentPoints(int count);
        public int GetPoints();
    }
}