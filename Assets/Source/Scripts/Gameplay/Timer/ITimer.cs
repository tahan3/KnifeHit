using Cysharp.Threading.Tasks;
using Source.Scripts.Reactive;

namespace Source.Scripts.Gameplay.Timer
{
    public interface ITimer<T>
    {
        public ReactiveVariable<T> Time { get; }
        
        public UniTaskVoid StartTimer();
        
        public void StopTimer();
    }
}