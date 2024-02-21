using Source.Scripts.Data.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.View.Buttons
{
    public class Switch : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private ToggleSpritesStorage sprites;
        
        [Header("Button")]
        [SerializeField] public Button button;
        
        [Header("State")]
        [SerializeField] private bool status = false;
        
        private void Awake()
        {
            button.image.sprite = sprites.GetSprite(status);
            button.image.SetNativeSize();
            button.onClick.AddListener(Click);
        }

        private void Click()
        {
            status = !status;
            button.image.sprite = sprites.GetSprite(status);
            button.image.SetNativeSize();
        }
        
        public void Enable()
        {
            status = true;
            button.image.sprite = sprites.GetSprite(status);
            button.image.SetNativeSize();
        }

        public void Disable()
        {
            status = false;
            button.image.sprite = sprites.GetSprite(status);
            button.image.SetNativeSize();
        }
    }
}