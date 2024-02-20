using Source.Scripts.Data.Profile;
using Source.Scripts.Load;
using Source.Scripts.Save;

namespace Source.Scripts.Profile
{
    public interface IProfileHandler : ILoader<ProfileData>, ISaver
    {
        public ProfileData Profile { get; set; }
    }
}