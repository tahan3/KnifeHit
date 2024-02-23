using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Sounds
{
    [CreateAssetMenu(fileName = "SoundsData", menuName = "SoundsData", order = 0)]
    public class SoundsData : KeyValueStorage<SoundType, AudioClip>
    {
    }
}