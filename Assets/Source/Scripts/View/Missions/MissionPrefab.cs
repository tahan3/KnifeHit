using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.View.Missions
{
    public class MissionPrefab : MonoBehaviour
    {
        public TextMeshProUGUI missionNameText;
        public TextMeshProUGUI playerCountText;
        public TextMeshProUGUI timeLimitText;
        public GameObject locker;
        public GameObject lockPlayButton;
        public Button playButton;
        public Image rewardIcon;
        public TextMeshProUGUI rewardText;
        public GameObject costContainer;
        public TextMeshProUGUI costText;

        public void Open()
        {
            locker.SetActive(false);
            lockPlayButton.SetActive(false);
            playButton.gameObject.SetActive(true);
        }

        public void Close()
        {
            locker.SetActive(true);
            lockPlayButton.SetActive(true);
            playButton.gameObject.SetActive(false);
        }

        public void SetCost(int cost)
        {
            costContainer.SetActive(true);
            costText.text = cost.ToString();
        }
    }
}