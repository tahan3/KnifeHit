using DG.Tweening;
using Source.Scripts.Data;
using Source.Scripts.Data.Profile;
using Source.Scripts.Load;
using Source.Scripts.Profile;
using Source.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class ProfileWindowHandler
    {
        private ProfileWindow _profileWindow;

        [Inject] private SoundsHandler _soundsHandler;
        
        public ProfileWindowHandler(ProfileWindow profileWindow)
        {
            _profileWindow = profileWindow;
        }

        public void Init()
        {
            _profileWindow.depositButton.onClick.AddListener(ShowDescription);
        }
        
        private void ShowDescription()
        {
            _soundsHandler.PlaySound(SoundType.Error);
            _profileWindow.depositDescription.DOKill();
            _profileWindow.depositDescription.DOFade(1f, 1f).onComplete += SetInvisible;
        }
        
        private void SetInvisible()
        {
            _profileWindow.depositDescription.DOFade(0f, 1f).SetDelay(2f);
        }
    }
}