using AYellowpaper.SerializedCollections;
using Source.Scripts.DailyReward;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Animations
{
    public class RewardAnimationsInstaller : MonoInstaller
    {
        [SerializeField] private SerializedDictionary<DailyRewardType, Image> prefabs;
        
        public override void InstallBindings()
        {
            var animationsHandler = new RewardAnimations(Container, prefabs, 10, transform);

            Container.Bind<RewardAnimations>().FromInstance(animationsHandler).AsSingle();
        }
    }
}