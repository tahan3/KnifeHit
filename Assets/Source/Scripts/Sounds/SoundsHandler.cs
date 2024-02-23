using System.Collections.Generic;
using Source.Scripts.Data;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using UnityEngine;

namespace Source.Scripts.Sounds
{
    public class SoundsHandler : ILoader<bool>
    {
        private Dictionary<SoundType, AudioSource> _sounds;
        
        public bool Mute { get; private set; }

        public SoundsHandler(KeyValueStorage<SoundType, AudioClip> sounds, AudioSource prefab, Transform parent = null)
        {
            Mute = Load();

            _sounds = new Dictionary<SoundType, AudioSource>();
            
            foreach (var soundType in sounds.GetKeys())
            {
                AudioClip clip;

                if (sounds.TryGetValue(soundType, out clip))
                {
                    AudioSource source = Object.Instantiate(prefab, parent);
                    source.playOnAwake = false;
                    source.clip = clip;

                    _sounds.Add(soundType, source);
                }
            }
        }
        
        public void PlaySound(SoundType type)
        {
            if (!Mute)
            {
                _sounds[type].Play();
            }
        }

        public bool Load()
        {
            return PlayerPrefs.GetInt(PrefsNames.Sounds.ToString(), 0) > 0;
        }
    }
}