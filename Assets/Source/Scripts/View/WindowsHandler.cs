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
        
        public WindowsHandler(KeyValueStorage<WindowType, AWindow> windowsStorage, DiContainer container, Transform parent = null)
        {
            _windows = new Dictionary<WindowType, AWindow>();
            
            foreach (var windowType in windowsStorage.GetKeys())
            {
                if (windowsStorage.TryGetValue(windowType, out var value))
                {
                    var item = container.InstantiatePrefabForComponent<AWindow>(value, parent);
                    item.Close();
                    
                    _windows.Add(windowType, item);
                }
            }
        }
        
        public WindowsHandler(KeyValueStorage<WindowType, AWindow> windowsStorage, DiContainer container, KeyValueStorage<BGWindowType, AWindow> bgWindows, Transform parent = null)
        {
            _windows = new Dictionary<WindowType, AWindow>();

            InitBGWindows(bgWindows, container, parent);
            
            foreach (var windowType in windowsStorage.GetKeys())
            {
                if (windowsStorage.TryGetValue(windowType, out var value))
                {
                    var item = container.InstantiatePrefabForComponent<AWindow>(value, parent);
                    item.Close();
                    
                    _windows.Add(windowType, item);
                }
            }
        }
        
        private void InitBGWindows(KeyValueStorage<BGWindowType, AWindow> bgWindows, DiContainer container, Transform parent)
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
            if (force)
            {
                foreach (var window in _windows.Values)
                {
                    window.Close();
                }
            }

            _currentWindow = _windows[type];
            _currentWindow.Open();
        }

        public void CloseWindow(WindowType type)
        {
            _currentWindow = _windows[type];
            _currentWindow.Close();
        }
    }
}