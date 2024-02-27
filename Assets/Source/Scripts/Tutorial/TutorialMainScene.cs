using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay;
using Source.Scripts.Prefs;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Tutorial
{
    public class TutorialMainScene : MonoBehaviour
    {
        [SerializeField] private GameObject _welcome;
        [SerializeField] private Button _buttonNo;
        [SerializeField] private Button _buttonYes;
        [SerializeField] private MissionConfig _missionConfig;

        [Inject] private MissionsHandler _missionsHandler;

        private void Awake()
        {
            if (!PlayerPrefs.HasKey(PrefsNames.TutorStage.ToString()))
            {
                _welcome.SetActive(true);

                _buttonNo.onClick.AddListener(OnClickNo);
                _buttonYes.onClick.AddListener(OnClickYes);
            }
        }

        private void OnClickNo()
        {
            _welcome.SetActive(false);
            PlayerPrefs.SetInt(PrefsNames.TutorStage.ToString(), 0);
        }

        private void OnClickYes()
        {
            _missionsHandler.LoadMission(_missionConfig, true);
        }
    }
}