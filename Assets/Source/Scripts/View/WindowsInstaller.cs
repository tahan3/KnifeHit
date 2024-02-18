using Source.Scripts.Data;
using Source.Scripts.Data.Screen;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View
{
    public class WindowsInstaller : MonoInstaller
    {
        [SerializeField] private KeyValueStorage<WindowType, AWindow> windowsData;
        [SerializeField] private KeyValueStorage<BGWindowType, AWindow> bgWindowsData;
        
        private WindowsHandler _windowsHandler;

        public override void InstallBindings()
        {
            _windowsHandler = bgWindowsData
                ? new WindowsHandler(windowsData, Container, bgWindowsData, transform)
                : new WindowsHandler(windowsData, Container, transform);

            Container.Bind<WindowsHandler>().FromInstance(_windowsHandler).AsSingle();
        }
    }
}