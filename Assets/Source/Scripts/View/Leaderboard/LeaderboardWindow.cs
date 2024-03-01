using Source.Scripts.View.Windows;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Leaderboard
{
    public class LeaderboardWindow : Window
    {
        public Transform itemsContainer;
        public Transform playerContainer;
        public Button button;

        private LeaderboardWindowHandler _windowHandler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _windowHandler = new LeaderboardWindowHandler(this);
            container.Inject(_windowHandler);
        }

        public override void Open()
        {
            _windowHandler.Init();
            
            base.Open();
        }
    }
}