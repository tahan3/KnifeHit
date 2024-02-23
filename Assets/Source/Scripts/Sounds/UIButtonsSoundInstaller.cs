using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Sounds
{
    public class UIButtonsSoundInstaller : MonoInstaller
    {
        [Inject] private SoundsHandler _soundsHandler;
        
        public override void InstallBindings()
        {
            foreach (var button in FindObjectsOfType<Button>())
            {
                button.onClick.AddListener(PlayUISound);
            }
        }

        private void PlayUISound()
        {
            _soundsHandler.PlaySound(SoundType.UIClick);
        }
    }
}
