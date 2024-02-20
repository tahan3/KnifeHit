using System.Collections.Generic;
using Source.Scripts.Data.UI;
using Source.Scripts.View.Profile;
using UnityEngine;

namespace Source.Scripts.Data.Profile
{
    [CreateAssetMenu(fileName = "ProfileStorage", menuName = "ProfileStorage", order = 0)]
    public class ProfileStorage : ScriptableObject
    {
        [Header("Profile icons")]
        public KeyValueStorage<int, Sprite> icons;
        
        [Header("Random nicknames")]
        public List<string> randomNicknames;

        [Header("Prefabs")] 
        public ProfileIcon profileIconPrefab;
        
        [Header("Kolhoz")]
        public ToggleSpritesStorage applyButtonSprites;
    }
}