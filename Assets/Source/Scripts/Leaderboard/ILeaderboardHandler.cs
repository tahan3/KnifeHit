namespace Source.Scripts.Leaderboard
{
    public interface ILeaderboardHandler
    {
        public void SetLeaderboard(LeaderboardType type, int stat);
    }
}