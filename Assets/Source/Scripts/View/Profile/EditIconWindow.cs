using Source.Scripts.Data.Profile;
using Source.Scripts.View.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class EditIconWindow : Window
    {
        [Header("Buttons")] 
        public Button applyButton;

        [Header("Containers")] 
        public Transform itemsParent;

        [Header("Profile Window")] 
        public ProfileWindow profileWindow;

        private EditIconWindowHandler _windowHandler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _windowHandler = new EditIconWindowHandler(this);
            container.Inject(_windowHandler);
        }

        public override void Open()
        {
            _windowHandler.Init();
            
            base.Open();
        }
    }
}