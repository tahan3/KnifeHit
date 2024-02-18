using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class ProfileWindow : Window
    {
        [Header("Containers")] 
        public AWindow mainWindowPrefab;
        public AWindow profileEditWindow;
    }
}