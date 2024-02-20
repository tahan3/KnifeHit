using Source.Scripts.Data.Profile;
using Source.Scripts.Load;
using UnityEngine;

namespace Source.Scripts.Profile
{
    public class ProfileLoader : ILoader<ProfileData>
    {
        public ProfileData Load()
        {
            return new ProfileData
            {
                Nickname = PlayerPrefs.GetString(ProfilePrefs.ProfileName.ToString(), "Name"),
                IconID = PlayerPrefs.GetInt(ProfilePrefs.ProfileIconID.ToString(), 0)
            };
        }
    }
}