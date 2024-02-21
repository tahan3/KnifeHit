using System.Collections.Generic;
using Source.Scripts.View.Windows;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Shop
{
    public class ShopWindow : Window
    {
        public Button depositButton;
        public Image depositDescription;

        public List<ShopItem> items;

        private ShopWindowHandler _windowHandler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _windowHandler = new ShopWindowHandler(this);
            container.Inject(_windowHandler);
            
            _windowHandler.Init();
        }
    }
}