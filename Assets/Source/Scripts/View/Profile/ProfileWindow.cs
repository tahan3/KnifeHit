using Source.Scripts.View.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class ProfileWindow : Window
    {
        [Header("Containers")] 
        public AWindow mainWindow;
        public AWindow profileEditWindow;

        [Header("Content")] 
        public Image depositDescription;
        public Button depositButton;
        
        public override void Open()
        {
            mainWindow.Open();
            profileEditWindow.Close();
            
            base.Open();
        }

        public override void Close()
        {
            mainWindow.Close();
            profileEditWindow.Close();
            
            base.Close();
        }

        [Inject]
        public void Construct(DiContainer container)
        {
            container.Inject(mainWindow);
            container.Inject(profileEditWindow);

            var handler = new ProfileWindowHandler(this);
            container.Inject(handler);
            handler.Init();
        }
    }
}