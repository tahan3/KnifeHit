using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.View.DailyReward
{
    public class DailyRewardItem : MonoBehaviour
    {
        public TextMeshProUGUI rewardNumber;
        public Image rewardIcon;
        public Button mainItem;
        public GameObject mask;
        public TextMeshProUGUI dayNumberText;

        public void SetActive(bool mode)
        {
            rewardNumber.gameObject.SetActive(mode);
            mainItem.enabled = mode;
            mask.SetActive(!mode);
            dayNumberText.gameObject.SetActive(mode);
        }
    }
}