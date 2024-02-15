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
        public void Construct(StageConfig stageConfig, LevelHandler levelHandler)
        {
            steps = new List<Image>();
            
            for (int i = 0; i < stageConfig.levels.Count - 1; i++)
            {
                steps.Add(Instantiate(levelPrefab, transform));
            }

            steps.Add(Instantiate(bossPrefab, transform));

            SetProgress(levelHandler.Level);
        }

        public override void SetProgress(int progress)
        {
            steps[progress].sprite = progress < steps.Count - 1
                ? spritesStorage.GetSprite(true)
                : bossSpritesStorage.GetSprite(true);
        }
    }
}