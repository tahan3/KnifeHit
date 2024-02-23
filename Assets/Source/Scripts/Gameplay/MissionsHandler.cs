using Source.Scripts.Counter;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay.Timer;
using Source.Scripts.Scene;
using UnityEngine.SceneManagement;

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

        public MissionsHandler()
        {
            _missionsCounter = new MissionsCounter();
            _missionsCounter.Counter.Value = 10;
        }
        
        public void LoadMission(MissionConfig mission)
        {
            _currentMission = mission;
            
            _currentTimer = new Timer.Timer(mission.time);
            _pointsCounter = new PositiveCounter();
            _multiplierHandler = new MultiplierHandler(1f, 5f, 0.1f);

            Level = 0;
            Stage = 0;

            SceneLoader.LoadScene("MainGameplay", StartMission);
        }

        public void StartMission()
        {
            _currentTimer.StartTimer();
        }
        
        public void EndMission()
        {
            _missionsCounter.Counter.Value++;
            _missionsCounter.Save();
            _currentTimer.StopTimer();
        }

        public void EndLevel()
        {
            Level++;

            if (Level >= _currentMission.stages[Stage].levels.Count)
            {
                Level = 0;
                Stage++;
            }
        }

        public void RestartWave()
        {
            Level = 0;
            SceneLoader.LoadScene("MainGameplay");
        }
    }
}