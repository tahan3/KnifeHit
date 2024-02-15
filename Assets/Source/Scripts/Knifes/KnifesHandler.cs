using Source.Scripts.Spawn;
using Source.Scripts.Thrower;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Knifes
{
    public class KnifesHandler
    {
        private Knife _currentKnife;

        private IThrower<Knife> _knifesThrower;

        public KnifesHandler(ISpawner<Knife> knifesSpawner, IThrower<Knife> knifesThrower, Button button)
        {
            knifesSpawner.OnSpawned += GetNewKnife;
            _knifesThrower = knifesThrower;
            button.onClick.AddListener(ThrowKnife);
        }

        private void ThrowKnife()
        {
            if (!_currentKnife) return;
            
            _knifesThrower.Throw(_currentKnife);
            _currentKnife = null;
        }
        
        private void GetNewKnife(Knife knife)
        {
            _currentKnife = knife;
        }
    }
}