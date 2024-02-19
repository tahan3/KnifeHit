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

        public int Level { get; private set; }
        public int Stage { get; private set; }
        public MissionConfig Mission => _currentMission;
        public ITimer Timer => _currentTimer;
        public ICounter PointsCounter => _pointsCounter;
        
        public void LoadMission(MissionConfig mission)
        {
            _currentMission = mission;
            
            _currentTimer = new Timer.Timer(mission.time);
            _pointsCounter = new PositiveCounter();

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
    }
}