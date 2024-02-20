using Source.Scripts.Gameplay;
using Source.Scripts.Leaderboard;
using Source.Scripts.Scene;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class YourScoreHandler
    {
        private YourScoreWindow _yourScoreWindow;

        [Inject] private MissionsHandler _missionsHandler;
        [Inject] private ILeaderboardHandler _leaderboardHandler;
        
        public YourScoreHandler(YourScoreWindow yourScoreWindow)
        {
            _yourScoreWindow = yourScoreWindow;
        }

        public void Init()
        {
            int yourScore = _missionsHandler.PointsCounter.CounterNumber.Value;
            int timeScore = _missionsHandler.Timer.Time.Value * 200;
            
            _yourScoreWindow.yourScore.text = yourScore.ToString();
            _yourScoreWindow.timeScore.text = timeScore.ToString();
            _yourScoreWindow.totalScore.text = (yourScore + timeScore).ToString();
            _yourScoreWindow.okButton.onClick.AddListener(OkButtonAction);
            
            _leaderboardHandler.SetLeaderboard(LeaderboardType.Points, yourScore + timeScore);
        }

        private void OkButtonAction()
        {
            _yourScoreWindow.okButton.onClick.RemoveListener(OkButtonAction);
            
            SceneLoader.LoadScene("MainMenu");
        }
    }
}