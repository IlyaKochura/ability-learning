using Code.Saves.Contracts;
using ModestTree;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Code.Saves.Implementation
{
    internal class SaveService : ISaveService
    {
        public void Save<T>(T saveModel) where T : class, ISaveModel
        {
            var save = JsonConvert.SerializeObject(saveModel);
            PlayerPrefs.SetString(typeof(T).Name, save);
            Debug.Log("Save");
        }

        public T GetSave<T>() where T : class , ISaveModel
        { 
            var saveString = PlayerPrefs.GetString(typeof(T).Name);

            if (string.IsNullOrEmpty(saveString))
            {
                return null;
            }

            var savedModel = JsonConvert.DeserializeObject<T>(saveString);

            return savedModel; 
        }
    }
}