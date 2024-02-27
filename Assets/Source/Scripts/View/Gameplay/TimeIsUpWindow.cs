using System;
using Source.Scripts.View.Windows;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeIsUpWindow : Window
    {
        public Button restartButton;
        public Button mainMenuButton;

        private TimeIsUpWindowHandler _handler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _handler = new TimeIsUpWindowHandler(this);
            container.Inject(_handler);
            _handler.Init();
        }

        public void OnDisable()
        {
            _handler.OnDisable();
        }
    }
}