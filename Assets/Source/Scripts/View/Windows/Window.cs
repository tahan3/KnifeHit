using System;
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

        public override bool IsActive()
        {
            return canvas.enabled;
        }
    }
}