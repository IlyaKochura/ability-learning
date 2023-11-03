namespace Code.Saves.Contracts
{
    public interface ISaveService
    {
        public void Save<T>(T saveModel) where T : class, ISaveModel;
        public T GetSave<T>() where T : class, ISaveModel;
    }
}