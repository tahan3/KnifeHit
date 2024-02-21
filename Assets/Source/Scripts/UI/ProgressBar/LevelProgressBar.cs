using System;
using System.Collections.Generic;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Data.UI;
using Source.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.UI.ProgressBar
{
    public class LevelProgressBar : StepProgress
    {
        [SerializeField] private Image levelPrefab;
        [SerializeField] private Image bossPrefab;
        [SerializeField] private ToggleSpritesStorage bossSpritesStorage;
        
        [Inject]
        public void Construct(MissionsHandler missionsHandler)
        {
            steps = new List<Image>();

            for (int i = 0; i < missionsHandler.Mission.stages[missionsHandler.Stage].levels.Count - 1; i++)
            {
                steps.Add(Instantiate(levelPrefab, transform));
            }

            steps.Add(Instantiate(bossPrefab, transform));

            SetProgress(missionsHandler.Level);
        }

        public override void SetProgress(int progress)
        {
            for (int i = 0; i < steps.Count - 1; i++)
            {
                steps[i].sprite = spritesStorage.GetSprite(progress >= i);
            }

            steps[^1].sprite = bossSpritesStorage.GetSprite(progress >= steps.Count - 1);
            /*steps[progress].sprite = progress < steps.Count - 1
                ? spritesStorage.GetSprite(true)
                : bossSpritesStorage.GetSprite(true);*/
        }
    }
}