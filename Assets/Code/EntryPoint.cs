using Code.Saves.Contracts;
using Code.Ui.Screens;
using ScreenManager.Runtime.Contracts;
using UnityEngine;
using Zenject;

namespace Code
{
    public class EntryPoint : MonoBehaviour
    {
        
        
        private ILoadSavesService[] _loadSavesServices;
        private IScreenManager _screenManager;

        [Inject]
        public void Constructor(ILoadSavesService[] loadSavesServices, IScreenManager screenManager)
        {
            _loadSavesServices = loadSavesServices;
            _screenManager = screenManager;
        }
        
        void Start()
        {
            _screenManager.ShowScreen<MainScreen>();
            
            foreach (var loadSavesService in _loadSavesServices)
            {
                loadSavesService.Load();
            }
        }
    }
}
