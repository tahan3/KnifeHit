using System.Collections.Generic;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Data.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.UI.ProgressBar
{
    public abstract class StepProgress : ProgressBar<int>
    {
        [SerializeField] protected ToggleSpritesStorage spritesStorage;

        protected List<Image> steps;
        
        public override void SetProgress(int fillValue)
        {
            fillValue = Mathf.Clamp(fillValue, 0, steps.Count);

            for (int i = 0; i < steps.Count; i++)
            {
                steps[i].sprite = spritesStorage.GetSprite(i <= fillValue - 1);
            }
        }
    }
}