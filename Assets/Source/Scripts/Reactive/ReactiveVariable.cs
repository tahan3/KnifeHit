using System;

namespace Source.Scripts.Reactive
{
    public class ReactiveVariable<T>
    {
        public event Action<T> OnValueChanged;

        public T Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        private T _value;

        public ReactiveVariable(T value)
        {
            _value = value;
        }

        public ReactiveVariable()
        {
            _value = default;
        }
    }
}