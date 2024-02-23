using System.Collections.Generic;
using Source.Scripts.Data;
using Source.Scripts.Data.Screen;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View
{
    public class WindowsHandler
    {
        private Dictionary<WindowType, AWindow> _windows;

        private AWindow _currentWindow;

        private Stack<AWindow> _prevWindows;
        
        public WindowsHandler()
        {
            _windows = new Dictionary<WindowType, AWindow>();
            _prevWindows = new Stack<AWindow>();
        }
        
        public void InitWindows(KeyValueStorage<WindowType, AWindow> windowsStorage, DiContainer container, Transform parent)
        {
            foreach (var windowType in windowsStorage.GetKeys())
            {
                if (windowsStorage.TryGetValue(windowType, out var value))
                {
                    AWindow item = Object.Instantiate(value, parent);
                    container.Inject(item);
                    //var item = container.InstantiatePrefabForComponent<AWindow>(value, parent);
                    item.Close();
                    
                    _windows.Add(windowType, item);
                }
            }
        }
        
        public void InitBGWindows(KeyValueStorage<BGWindowType, AWindow> bgWindows, DiContainer container, Transform parent)
        {
            foreach (var windowType in bgWindows.GetKeys())
            {
                if (bgWindows.TryGetValue(windowType, out var value))
                {
                    var item = container.InstantiatePrefabForComponent<AWindow>(value, parent);
                    item.Open();
                }
            }
        }
        
        public void OpenWindow(WindowType type, bool force = false)
        {
            if (_currentWindow == _windows[type]) return;
            
            if (force)
            {
                ForceClose();
            }

            if (_currentWindow)
            {
                _prevWindows.Push(_currentWindow);
            }

            _currentWindow = _windows[type];
            _currentWindow.Open();
        }

        public void OpenPreviousWindow(bool force = false)
        {
            if (force)
            {
                ForceClose();
            }

            _currentWindow = _prevWindows.Pop();
            
            if (_currentWindow)
            {
                _currentWindow.Open();
            }
        }
        
        public void CloseWindow(WindowType type)
        {
            _currentWindow = _windows[type];
            _currentWindow.Close();
        }

        private void ForceClose()
        {
            foreach (var window in _windows.Values)
            {
                window.Close();
            }
        }
    }
}