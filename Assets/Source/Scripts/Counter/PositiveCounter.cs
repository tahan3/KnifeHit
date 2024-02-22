using Source.Scripts.Reactive;
using UnityEngine;

namespace Source.Scripts.Counter
{
    public class PositiveCounter : ICounter
    {
        public ReactiveVariable<int> Counter { get; }

        public PositiveCounter()
        {
            Counter = new ReactiveVariable<int>();
            Counter.OnValueChanged += OnlyPositive;
        }

        private void OnlyPositive(int value)
        {
            if (value < 0)
            {
                Counter.Value = 0;
            }
        }
    }
}