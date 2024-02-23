using Source.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Windows
{
    public class SoundWindow : Window
    {
        [SerializeField] private SoundType soundType;

        [Inject] private SoundsHandler _soundsHandler;
        
        public override void Open()
        {
            _soundsHandler.PlaySound(soundType);
            
            base.Open();
        }
    }
}