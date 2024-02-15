using Source.Scripts.Reactive;
using UnityEngine;

namespace Source.Scripts.Counter
{
    public class PositiveCounter : ICounter
    {
        public ReactiveVariable<int> CounterNumber { get; }

        public PositiveCounter()
        {
            CounterNumber = new ReactiveVariable<int>();
            CounterNumber.OnValueChanged += OnlyPositive;
        }

        private void OnlyPositive(int value)
        {
            if (value < 0)
            {
                CounterNumber.Value = 0;
            }
        }
    }
}