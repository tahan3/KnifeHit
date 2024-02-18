using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Scripts.Reactive;
using UnityEngine;

namespace Source.Scripts.Gameplay.Timer
{
    public class Timer : ITimer
    {
        public ReactiveVariable<int> Time { get; private set; }

        private CancellationTokenSource _cancellationToken;
        
        public Timer(int seconds)
        {
            Time = new ReactiveVariable<int>(seconds);
        }
        
        public async void StartTimer()
        {
            _cancellationToken = new CancellationTokenSource();
            
            try
            {
                while (Time.Value > 0)
                {
                    await UniTask.WaitForSeconds(1, cancellationToken: _cancellationToken.Token);

                    Time.Value--;
                }
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e.Message);
            }
        }

        public void StopTimer()
        {
            _cancellationToken.Cancel();
        }
    }
}