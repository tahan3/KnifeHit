using Source.Scripts.Data.Screen;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Windows
{
    public class ErrorWindow : SoundWindow
    {
        public Button okButton;
        
        [Inject]
        private void Construct(WindowsHandler windowsHandler)
        {
            okButton.onClick.AddListener(() => windowsHandler.CloseWindow(WindowType.Error));
        }
    }
}