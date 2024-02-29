using DG.Tweening;
using Source.Scripts.Data.Screen;
using Source.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Shop
{
    public class ShopWindowHandler
    {
        private ShopWindow _shopWindow;

        [Inject] private WindowsHandler _windowsHandler;
        [Inject] private SoundsHandler _soundsHandler;
        
        public ShopWindowHandler(ShopWindow shopWindow)
        {
            _shopWindow = shopWindow;
        }

        public void Init()
        {
            _shopWindow.depositButton.onClick.AddListener(ShowDescription);

            for (int i = 0; i < _shopWindow.items.Count; i++)
            {
                _shopWindow.items[i].buyButton.onClick.AddListener(ShowError);
            }
        }

        private void ShowError()
        {
            _windowsHandler.OpenWindow(WindowType.Error);
        }
        
        private void ShowDescription()
        {
            _soundsHandler.PlaySound(SoundType.Error);
            _shopWindow.depositDescription.DOKill();
            _shopWindow.depositDescription.DOFade(1f, 1f).onComplete += SetInvisible;
        }

        private void SetInvisible()
        {
            _shopWindow.depositDescription.DOFade(0f, 1f).SetDelay(2f);
        }
    }
}