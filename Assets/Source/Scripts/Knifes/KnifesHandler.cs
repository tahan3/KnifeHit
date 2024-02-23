using System;
using Source.Scripts.Spawn;
using Source.Scripts.Thrower;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Scripts.Knifes
{
    public class KnifesHandler
    {
        private Knife _currentKnife;

        private IThrower<Knife> _knifesThrower;

        public KnifesHandler(ISpawner<Knife> knifesSpawner, IThrower<Knife> knifesThrower, ClickPanel clickPanel)
        {
            knifesSpawner.OnSpawned += GetNewKnife;
            _knifesThrower = knifesThrower;
            clickPanel.OnClick += ThrowKnife;
        }

        private void ThrowKnife()
        {
            if (!_currentKnife) return;

            _currentKnife.SetActive(true);
            _knifesThrower.Throw(_currentKnife);
            _currentKnife = null;
        }
        
        private void GetNewKnife(Knife knife)
        {
            _currentKnife = knife;
        }
    }
}