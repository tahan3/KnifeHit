using System.Collections.Generic;
using Source.Scripts.Data.LevelData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.UI.ProgressBar
{
    public class KnifesStepProgressBar : StepProgress
    {
        [SerializeField] private Image stepPrefab;
        
        [Inject]
        public void Construct(LevelConfig levelConfig)
        {
            steps = new List<Image>();
            
            for (int i = 0; i < levelConfig.knifesToWin; i++)
            {
                steps.Add(Instantiate(stepPrefab, transform));
            }
        }
    }
}