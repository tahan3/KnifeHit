using Source.Scripts.Events;
using Source.Scripts.Knifes;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Aim.Bonuses
{
    public class Bonus : KnifeAim
    {
        [Inject] private MainEventsHandler _mainEvents;
        
        public override void GetKnife(Knife knife)
        {
            _mainEvents.OnKnifeHitBonus?.Invoke();
            
            gameObject.SetActive(false);
        }
    }
}