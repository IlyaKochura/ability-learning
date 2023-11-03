using Code.Saves.Contracts;
using ModestTree;
using Newtonsoft.Json;
using UnityEngine;

namespace Code.Saves.Implementation
{
    public class SaveService : ISaveService
    {
        public void Save<T>(T saveModel) where T : class, ISaveModel
        {
            var save = JsonConvert.SerializeObject(saveModel);
            PlayerPrefs.SetString(typeof(T).Name, save);
        }

        public T GetSave<T>() where T : class , ISaveModel
        { 
            var saveString = PlayerPrefs.GetString(typeof(T).Name);

            if (saveString.IsEmpty() || saveString == null)
            {
                return null;
            }

            var savedModel = JsonConvert.DeserializeObject<T>(saveString);

            if (savedModel == null)
            {
                return null;
            }

            return savedModel; 
        }
    }
}