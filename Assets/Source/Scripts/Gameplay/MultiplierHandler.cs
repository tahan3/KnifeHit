using Source.Scripts.Reactive;

namespace Source.Scripts.Gameplay
{
    public class MultiplierHandler
    {
        private readonly float _maxMultiplier;
        private readonly float _minMultiplier;
        private readonly float _perIncrement;
        
        public ReactiveVariable<float> Multiplier { get; }
        
        public MultiplierHandler(float minMultiplier, float maxMultiplier, float incrementValue)
        {
            Multiplier = new ReactiveVariable<float>(minMultiplier);
            
            _maxMultiplier = maxMultiplier;
            _minMultiplier = minMultiplier;
            _perIncrement = incrementValue;
        }

        public void IncreaseMultiplier()
        {
            Multiplier.Value += _perIncrement;

            if (Multiplier.Value > _maxMultiplier)
            {
                Multiplier.Value = _maxMultiplier;
            }
        }

        public void SetDefaultMultiplier()
        {
            Multiplier.Value = _minMultiplier;
        }
    }
}