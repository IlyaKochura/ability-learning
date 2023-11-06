using System;
using System.Collections.Generic;
using ScreenManager.Runtime.Contracts;
using UnityEngine;
using Zenject;

namespace ScreenManager.Runtime.Implementation
{
    public class ScreenManager : IScreenManager
    {
        private readonly Dictionary<Type, IUIScreen> _screens = new();
        private readonly Stack<IUIScreen> _showedScreenStack = new();

        public void ShowScreen<T>() where T : IUIScreen
        {
            var screenType = typeof(T);

            var screen = GetScreen(screenType);

            if (screen == null)
            {
                Debug.LogError($"Not found screen: {screenType.Name}");
                return;
            }

            _showedScreenStack.TryPeek(out var lastShowedScreen);

            if (lastShowedScreen != null && lastShowedScreen.GetType() == screenType)
            {
                Debug.Log($"Screen: {screenType} is already opened");
                return;
            }
            
            _showedScreenStack.Push(screen);
            
            screen.Show();
        }

        public void HideScreen<T>() where T : IUIScreen
        {
            var screenType = typeof(T);

            var screen = GetScreen(screenType);

            if (!_showedScreenStack.Contains(screen) && _showedScreenStack.Peek().GetType() != screenType)
            {
                Debug.LogError($"Screen: {screenType} not possible hide");
                return;
            }

            _showedScreenStack.TryPeek(out var uiScreen);
            
            uiScreen.Hide();
        }

        public void GoBack()
        {
            var screen = _showedScreenStack.Pop();

            if (screen == null)
            {
                Debug.LogError($"All screens is closed");
                return;
            }
            
            screen.Hide();
        }
        

        public void Resolve(DiContainer diContainer)
        {
            var screens = diContainer.ResolveAll<IUIScreen>();
            
            screens.ForEach(screen => _screens.Add(screen.GetType(), screen));
        }
        
        private IUIScreen GetScreen(Type screenType)
        {
            if (_screens.TryGetValue(screenType, out var uiScreen))
            {
                return uiScreen;
            }
                
            Debug.LogError($"ScreenManager, not found screen with Type {screenType}");

            return null;
        }
        
    }
}