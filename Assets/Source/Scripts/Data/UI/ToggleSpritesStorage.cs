using UnityEngine;

namespace Source.Scripts.Data.UI
{
    [CreateAssetMenu(fileName = "ToggleSpritesStorage", menuName = "ToggleSpritesStorage", order = 0)]
    public class ToggleSpritesStorage : ScriptableObject
    {
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;

        public Sprite GetSprite(bool state)
        {
            return state ? onSprite : offSprite;
        }
    }
}