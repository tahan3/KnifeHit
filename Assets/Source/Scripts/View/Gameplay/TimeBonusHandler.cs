using System.Text;
using Cysharp.Threading.Tasks;
using Source.Scripts.Data.Screen;
using Source.Scripts.Gameplay;
using Source.Scripts.Sounds;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeBonusHandler
    {
        private TimeBonusWindow _timeBonusWindow;

        private readonly float _totalDelay;
        private readonly int _bonusPerSecond;

        private MissionsHandler _missionsHandler;
        private WindowsHandler _windowsHandler;
        private SoundsHandler _soundsHandler;
        private GameOverHandler _gameOverHandler;
        
        
        public TimeBonusHandler(TimeBonusWindow timeBonusWindow, int bonusPerSecond = 200, float totalDelay = 2f)
        {
            _timeBonusWindow = timeBonusWindow;
            _bonusPerSecond = bonusPerSecond;
            _totalDelay = totalDelay;
        }

        [Inject]
        public void Construct(MissionsHandler missionsHandler, GameOverHandler gameOverHandler,
            WindowsHandler windowsHandler, SoundsHandler soundsHandler)
        {
            _missionsHandler = missionsHandler;
            _windowsHandler = windowsHandler;
            _soundsHandler = soundsHandler;
            _gameOverHandler = gameOverHandler;
            
            gameOverHandler.OnMissionEnded += () => windowsHandler.OpenWindow(WindowType.TimeBonus, true);
        }
        
        public async UniTaskVoid Init()
        {
            int time = _missionsHandler.Timer.Time.Value;
            int points = 0;
            float timer = _totalDelay;

            _soundsHandler.PlaySound(SoundType.PointsPerTime);
            
            while (time >= 0 && timer > 0)
            {
                _timeBonusWindow.seconds.text = time + " sec";
                _timeBonusWindow.points.text = points.ToString();

                await UniTask.Yield();

                time--;
                timer -= Time.deltaTime;
                points += _bonusPerSecond;
            }
            
            _timeBonusWindow.seconds.text = 0 + " sec";
            _timeBonusWindow.points.text = (_missionsHandler.Timer.Time.Value * _bonusPerSecond).ToString();
            
            await UniTask.WaitForSeconds(0.5f);

            _gameOverHandler.OnTimerEnded?.Invoke();

            _windowsHandler.OpenWindow(WindowType.YourScore, true);
        }
    }
}