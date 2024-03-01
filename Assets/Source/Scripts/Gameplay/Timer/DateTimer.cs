using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Source.Extension;
using Source.Scripts.DailyReward;
using Source.Scripts.Load;
using Source.Scripts.Reactive;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Gameplay.Timer
{
    public class DateTimer : ITimer<DateTime>, ILoader<DateTime>, ISaver
    {
        public ReactiveVariable<DateTime> Time { get; private set; }

        private readonly string _timerKey;

        private CancellationTokenSource _cancellationToken;
        
        public DateTimer(string timerKey)
        {
            _timerKey = timerKey;

            Time = new ReactiveVariable<DateTime>(Load());
        }

        public void SetDelay(DateTime delay)
        {
            Time = new ReactiveVariable<DateTime>(delay);
            Save();
        }
        
        public async UniTaskVoid StartTimer()
        {
            _cancellationToken = new CancellationTokenSource();
            var cachedNow = DateTime.Now;
            
            try
            {
                while (Time.Value >= cachedNow)
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
            _cancellationToken?.Cancel();
        }

        public DateTime Load()
        {
            return PlayerPrefs.HasKey(_timerKey)
                ? DateTime.FromBinary(
                    Convert.ToInt64(PlayerPrefs.GetString(_timerKey, DateTime.MinValue.ToBinary().ToString())))
                : DateTime.MinValue;
        }

        public void Save()
        {
            PlayerPrefs.SetString(_timerKey, Time.Value.ToBinary().ToString());
        }
    }
}