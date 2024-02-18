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

        public MissionConfig Mission => _currentMission;
        public ITimer Timer => _currentTimer;
        
        public void SetMission(MissionConfig mission)
        {
            _currentMission = mission;
            _currentTimer = new Timer.Timer(mission.time);

            SceneLoader.LoadScene("MainGameplay");
        }
    }
}