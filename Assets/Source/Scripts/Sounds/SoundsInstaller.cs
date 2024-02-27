using Source.Scripts.Data;
using Source.Scripts.UI.Vibration;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Sounds
{
    public class SoundsInstaller : MonoInstaller
    {
        [SerializeField] private KeyValueStorage<SoundType, AudioClip> sounds;
        [SerializeField] private AudioSource prefab;

        public override void InstallBindings()
        {
            var soundsHandler = new SoundsHandler(sounds, prefab, transform);

            Container.Bind<SoundsHandler>().FromInstance(soundsHandler).AsSingle();
            Container.Bind<VibrationHandler>().FromNew().AsSingle();
        }
    }
}