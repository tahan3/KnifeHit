using Source.Scripts.Data;
using Source.Scripts.Data.Profile;
using Source.Scripts.Load;
using UnityEngine;

namespace Source.Scripts.View.Profile
{
    public class ProfileHandler
    {
        private KeyValueStorage<int, Sprite> _iconsStorage;
        private ProfileWindow _profileWindow;
        
        private ProfileData _profileData;

        public ProfileHandler(ILoader<ProfileData> profileLoader, KeyValueStorage<int, Sprite> iconsStorage, ProfileWindow profileWindow)
        {
            _profileData = profileLoader.Load();
            _iconsStorage = iconsStorage;
            _profileWindow = profileWindow;
        }
    }
}