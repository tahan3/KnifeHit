using Source.Scripts.View.Windows;
using UnityEngine;

namespace Source.Scripts.View
{
    public class PauseWindow : Window
    {
        public override void Open()
        {
            Time.timeScale = 0f;
            
            base.Open();
        }

        public override void Close()
        {
            Time.timeScale = 1f;

            base.Close();
        }
    }
}