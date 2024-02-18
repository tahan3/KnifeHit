using Source.Scripts.Reactive;

namespace Source.Scripts.Gameplay.Timer
{
    public interface ITimer
    {
        public ReactiveVariable<int> Time { get; }
        
        public void StartTimer();
        
        public void StopTimer();
    }
}