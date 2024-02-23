using Source.Scripts.View.Windows;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeIsUpWindow : Window
    {
        public Button restartButton;
        public Button mainMenuButton;

        [Inject]
        public void Construct(DiContainer container)
        {
            var handler = new TimeIsUpWindowHandler(this);
            container.Inject(handler);
            handler.Init();
        }
    }
}