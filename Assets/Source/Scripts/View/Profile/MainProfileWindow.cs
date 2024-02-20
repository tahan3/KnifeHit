using System.Runtime.InteropServices;
using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class MainProfileWindow : Window
    {
        [Header("Profile Picture")]
        public Image profileImg;
        public Button editButton;

        [Header("Nickname")] 
        public TMP_InputField nicknameInput;
        public Button randomNicknameButton;

        [Header("Apply button")]
        public Button applyButton;
        
        [Header("Profile Window")] 
        public ProfileWindow profileWindow;

        private MainProfileWindowHandler _windowHandler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _windowHandler = new MainProfileWindowHandler(this);
            container.Inject(_windowHandler);
        }
        
        public override void Open()
        {
            _windowHandler.Init();
            
            base.Open();
        }
    }
}