using UnityEngine;

namespace Source.Scripts.Data.Profile
{
    [CreateAssetMenu(fileName = "ProfileIcons", menuName = "ProfileIcons", order = 0)]
    public class ProfileIcons : KeyValueStorage<int, Sprite>
    {
    }
}