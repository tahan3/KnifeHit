using UnityEngine;

namespace Source.Scripts.View.Windows
{
    public class Window : AWindow
    {
        [SerializeField] private Canvas canvas;
        
        public override void Open()
        {
            canvas.enabled = true;
        }

        public override void Close()
        {
            canvas.enabled = false;
        }

        public void Redraw()
        {
            var state = gameObject.activeSelf;
            gameObject.SetActive(!state);
            gameObject.SetActive(state);
        }
    }
}