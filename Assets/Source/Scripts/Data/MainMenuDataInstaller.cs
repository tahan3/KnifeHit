using Source.Scripts.Data.Leaderboard;
using Source.Scripts.Data.Profile;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Data
{
    public class MainMenuDataInstaller : MonoInstaller
    {
        [SerializeField] private ProfileStorage _profileData;
        [SerializeField] private LeaderboardData _leaderboardData;
        
        public override void InstallBindings()
        {
            Container.Bind<ProfileStorage>().FromInstance(_profileData).AsSingle();
            Container.Bind<LeaderboardData>().FromInstance(_leaderboardData).AsSingle();
        }
    }
}