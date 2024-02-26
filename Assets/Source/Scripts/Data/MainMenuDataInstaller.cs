using Source.Scripts.Data.Leaderboard;
using Source.Scripts.Data.LevelReward;
using Source.Scripts.Data.Profile;
using Source.Scripts.View.DailyReward;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Data
{
    public class MainMenuDataInstaller : MonoInstaller
    {
        [SerializeField] private ProfileStorage _profileData;
        [SerializeField] private LeaderboardData _leaderboardData;
        [SerializeField] private LevelRewardConfig _levelRewardConfig;
        [SerializeField] private LevelRewardWindowData _levelRewardWindowData;
        [SerializeField] private DailyRewardWindowData _dailyRewardWindowData;
        
        public override void InstallBindings()
        {
            Container.Bind<ProfileStorage>().FromInstance(_profileData).AsSingle();
            Container.Bind<LeaderboardData>().FromInstance(_leaderboardData).AsSingle();
            Container.Bind<LevelRewardConfig>().FromInstance(_levelRewardConfig).AsSingle();
            Container.Bind<LevelRewardWindowData>().FromInstance(_levelRewardWindowData).AsSingle();
            Container.Bind<DailyRewardWindowData>().FromInstance(_dailyRewardWindowData).AsSingle();
        }
    }
}