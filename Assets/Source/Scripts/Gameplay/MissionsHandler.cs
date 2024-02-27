using Source.Scripts.Counter;
using Source.Scripts.Currency;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay.Timer;
using Source.Scripts.Level;
using Source.Scripts.Scene;
using UnityEngine.SceneManagement;
using Zenject;

namespace Source.Scripts.Gameplay
{
    public class MissionsHandler
    {
        private MissionConfig _currentMission;
        private ITimer _currentTimer;
        private ICounter _pointsCounter;
        private MissionsCounter _missionsCounter;
        private MultiplierHandler _multiplierHandler;

        public int Level { get; private set; }
        public int Stage { get; private set; }
        public MissionConfig Mission => _currentMission;
        public ITimer Timer => _currentTimer;
        public ICounter PointsCounter => _pointsCounter;
        public MultiplierHandler Multiplier => _multiplierHandler;
        public int OpenedMissions => _missionsCounter.Counter.Value;

        [Inject] private CurrencyHandler _currencyHandler;
        [Inject] private ExpHandler _expHandler;

        private const int ExpPerMission = 25;

        public MissionsHandler()
        {
            _missionsCounter = new MissionsCounter();
        }

        public void LoadMission(MissionConfig mission, bool isTutor = false)
        {
            _currentMission = mission;

            _currentTimer = new Timer.Timer(mission.time);
            _pointsCounter = new PositiveCounter();
            _multiplierHandler = new MultiplierHandler(1f, 5f, 0.1f);

            Level = 0;
            Stage = 0;

            if (isTutor)
            {
                SceneLoader.LoadScene("MainGameplay_RomaTest", StartMission);
            }
            else
            {
                SceneLoader.LoadScene("MainGameplay", StartMission);
            }
        }

        public void StartMission()
        {
            _currentTimer.StartTimer();
        }

        public void EndMission()
        {
            AddRewards();
            _expHandler.GetExp(ExpPerMission);
            _missionsCounter.TrySaveProgress(Mission);
            _currentTimer.StopTimer();
        }

        private void AddRewards()
        {
            _currencyHandler.Currencies[_currentMission.reward.currency].Counter.Value += _currentMission.reward.amount;

            _currencyHandler.Save();
        }

        public void EndLevel()
        {
            Level++;

            if (Level >= _currentMission.stages[Stage].levels.Count)
            {
                Level = 0;
                Stage++;
            }
            
            _currentTimer.StopTimer();
        }

        public void RestartWave()
        {
            Level = 0;
            _currentTimer.StopTimer();
            //SceneLoader.LoadScene("MainGameplay_RomaTest");
            //SceneLoader.LoadScene("MainGameplay");
            SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}