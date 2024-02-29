using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Extension;
using Source.Scripts.DailyReward;
using Source.Scripts.Load;
using Source.Scripts.Reactive;
using UnityEngine;

namespace Source.Scripts.Gameplay.Timer
{
    public class DateTimer : ITimer<DateTime>, ILoader<DateTime>
    {
        public ReactiveVariable<DateTime> Time { get; private set; }

        private readonly string _timerKey;

        private CancellationTokenSource _cancellationToken;
        
        public DateTimer(string timerKey, DateTime delay)
        {
            _timerKey = timerKey;

            Time = new ReactiveVariable<DateTime>(delay);
        }
        
        public async void StartTimer()
        {
            _cancellationToken = new CancellationTokenSource();
            
            try
            {
                while (Time.Value >= DateTime.Now)
                {
                    await UniTask.WaitForSeconds(1, cancellationToken: _cancellationToken.Token);

                    Time.Value = Time.Value.AddSeconds(-1).Trim(TimeSpan.TicksPerSecond);
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

        public DateTime Load()
        {
            return DateTime.FromBinary(
                Convert.ToInt64(PlayerPrefs.GetString(_timerKey, DateTime.MinValue.ToBinary().ToString())));
        }
    }
}