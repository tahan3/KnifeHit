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

        private readonly float _delay;
        private readonly int _bonusPerSecond;

        private MissionsHandler _missionsHandler;
        private WindowsHandler _windowsHandler;
        private SoundsHandler _soundsHandler;
        
        
        public TimeBonusHandler(TimeBonusWindow timeBonusWindow, int bonusPerSecond = 200, float delay = 1f)
        {
            _timeBonusWindow = timeBonusWindow;
            _bonusPerSecond = bonusPerSecond;
            _delay = delay;
        }

        [Inject]
        public void Construct(MissionsHandler missionsHandler, GameOverHandler gameOverHandler,
            WindowsHandler windowsHandler, SoundsHandler soundsHandler)
        {
            _missionsHandler = missionsHandler;
            _windowsHandler = windowsHandler;
            _soundsHandler = soundsHandler;
            
            gameOverHandler.OnMissionEnded += () => windowsHandler.OpenWindow(WindowType.TimeBonus, true);
        }
        
        public async UniTaskVoid Init()
        {
            int time = _missionsHandler.Timer.Time.Value;
            int points = 0;

            _soundsHandler.PlaySound(SoundType.PointsPerTime);
            
            while (time >= 0)
            {
                _timeBonusWindow.seconds.text = time + " sec";
                _timeBonusWindow.points.text = points.ToString();

                await UniTask.WaitForSeconds(_delay / _missionsHandler.Timer.Time.Value, true);

                time--;
                points += _bonusPerSecond;
            }

            await UniTask.WaitForSeconds(0.5f);

            _windowsHandler.OpenWindow(WindowType.YourScore, true);
        }
    }
}