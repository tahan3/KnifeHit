using Source.Scripts.Reactive;

namespace Source.Scripts.Counter
{
    public interface ICounter
    {
        public ReactiveVariable<int> Counter { get; }
    }
}