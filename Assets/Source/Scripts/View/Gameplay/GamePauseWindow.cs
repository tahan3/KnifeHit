using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class GamePauseWindow : PauseWindow
    {
        public Button resumeButton;
        public Button restartButton;
        public Button exitButton;

        [Inject]
        public void Construct(DiContainer container)
        {
            var handler = new GamePauseWindowHandler(this);
            container.Inject(handler);
            handler.Init();
        }
    }
}