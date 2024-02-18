using Source.Scripts.Data.Profile;
using UnityEngine;

namespace Source.Scripts.Load
{
    public class ProfileLoader : ILoader<ProfileData>
    {
        public ProfileData Load()
        {
            return new ProfileData
            {
                Nickname = PlayerPrefs.GetString(ProfilePrefs.ProfileName.ToString(), "Keki4"),
                IconID = PlayerPrefs.GetInt(ProfilePrefs.ProfileIconID.ToString(), 0)
            };
        }
    }
}