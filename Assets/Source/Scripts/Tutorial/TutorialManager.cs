using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Source.Scripts.Gameplay;
using Zenject;
using System;
using Source.Scripts.UI;
using Source.Scripts.Counter;
using System.Linq;
using UnityEngine.UI;
using Source.Scripts.Prefs;

namespace Source.Scripts.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private Button _buttonClick;
        [SerializeField] private ClickPanel _clickPanel;

        [Inject] private MissionsHandler _missionsHandler;
        [Inject] private KnifesPerRoundCounter _knifesPerRoundCounter;
        [Inject] private GameOverHandler _gameOverHandler;

        private int _tutorStage;
        [SerializeField] private List<TutorialStage> _stages = new List<TutorialStage>();

        private void Awake()
        {
            _tutorStage = PlayerPrefs.GetInt(PrefsNames.TutorStage.ToString(), 3);

            _gameOverHandler.OnLevelEnded += OnLevelEnd;

            _buttonClick.onClick.AddListener(OnClicked);
        }

        private void OnDestroy()
        {
            _gameOverHandler.OnLevelEnded += OnLevelEnd;

            PlayerPrefs.SetInt(PrefsNames.TutorStage.ToString(), _tutorStage);
        }

        private void Start()
        {
            if (_missionsHandler.Level == 0)
            {
                StartCoroutine(Delayed(Stage4, 0.7f));
                _knifesPerRoundCounter.Counter.OnValueChanged += OnValueChangedKnifes;
            }

            if (_missionsHandler.Level == 1)
            {
                StartCoroutine(Delayed(Stage11, 0.5f));
                _clickPanel.OnClick += OnClicked;
            }

            if (_missionsHandler.Level == 2)
            {
                StartCoroutine(Delayed(Stage15, 0.6f));
                _clickPanel.OnClick += OnClicked;
            }

            if(_missionsHandler.Level == 3)
            {
                StartCoroutine(Delayed(Stage19, 0.5f));
            }

            _buttonClick.gameObject.SetActive(false);
        }

        private void Stage4()
        {
            ShowStage(4);
        }

        private void Stage6()
        {
            ShowStage(6);
            _knifesPerRoundCounter.Counter.OnValueChanged -= OnValueChangedKnifes;
        }

        private void Stage7()
        {
            ShowStage(7);
        }

        private void Stage9()
        {
            StartCoroutine(Delayed(() => ShowStage(9), 0.15f));
        }

        private void Stage11()
        {
            ShowStage(11, false);
        }

        private void Stage13()
        {
            ShowStage(13);
        }

        private void Stage15()
        {
            ShowStage(15, false);
        }

        private void Stage17()
        {
            ShowStage(17);
        }

        private void Stage19()
        {
            ShowStage(19);
        }

        private void Stage23()
        {
            StartCoroutine(Delayed(() => ShowStage(23), 2.1f));
        }

        private void Stage24()
        {
            ShowStage(24);
        }

        private void ShowStage(byte numberStage, bool withButton = true)
        {
            if (withButton)
                _buttonClick.gameObject.SetActive(true);

            FreezeTheGame();
            _stages.First(x => x.StageNumber == numberStage).ActivateTutor();
            _tutorStage = numberStage;
        }

        [Button]
        public void FreezeTheGame()
        {
            Time.timeScale = 0;
        }

        [Button]
        public void ContinueTheGame()
        {
            Time.timeScale = 1;
        }

        private void OnClicked()
        {
            for (int i = 0; i < _stages.Count; i++)
            {
                if (_stages[i].IsActive)
                {
                    _stages[i].DeactivateTutor();

                    if (_stages[i].StageNumber == 6)
                    {
                        Stage7();
                        break;
                    }

                    if (_stages[i].StageNumber == 11)
                    {
                        _clickPanel.OnClick -= OnClicked;
                        StartCoroutine(Delayed(Stage13, 0.2f));
                    }

                    if (_stages[i].StageNumber == 15)
                    {
                        _clickPanel.OnClick -= OnClicked;
                        StartCoroutine(Delayed(Stage17, 0.2f));
                    }

                    if (_stages[i].StageNumber == 23)
                    {
                        Stage24();
                        break;
                    }

                    _buttonClick.gameObject.SetActive(false);
                    ContinueTheGame();
                }
            }
        }

        private void OnValueChangedKnifes(int value)
        {
            Stage6();
        }


        private void OnLevelEnd()
        {
            if (_missionsHandler.Level == 0)
                Stage9();

            if (_missionsHandler.Level == 6)
                Stage23();
        }

        private IEnumerator Delayed(Action task, float delay, bool unscaledTime = true)
        {
            float timer = 0f;

            while (timer < delay)
            {
                timer += unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

                yield return null;
            }
            task?.Invoke();
        }
    }
}
