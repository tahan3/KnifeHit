using Source.Scripts.UI.ProgressBar;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class DefaultProgressBar : ProgressBar<float>
    {
        public Image imageToFill;
        
        public override void SetProgress(float fillValue)
        {
            imageToFill.fillAmount = fillValue;
        }
    }
}