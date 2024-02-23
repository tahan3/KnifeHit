using DG.Tweening;
using Source.Scripts.UI.ProgressBar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.View.LevelReward
{
    public class LevelRewardProgressBar : ProgressBar<int>
    {
        [Header("Content")] 
        public Image rewardIcon;
        public TextMeshProUGUI rewardText;
        public GameObject canTakeMark;
        public GameObject rewardTakenMark;
        
        [Header("Progress")]
        public Image fillImage;
        
        public override void SetProgress(int fillValue)
        {
            float fill = fillValue / 100f;

            fillImage.fillAmount = fill;
        }
    }
}