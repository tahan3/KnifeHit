using UnityEngine;
#if UNITY_IOS
// Include the IosSupport namespace if running on iOS:
using Unity.Advertisement.IosSupport;
#endif

namespace Source.Scripts.ATT
{
    public static class ATTHandler
    {
        public static void ShowATT()
        {
#if UNITY_IOS
            Debug.Log("Show ATT");
            // Check the user's consent status.
            // If the status is undetermined, display the request request:
            if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
                ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
            {
                ATTrackingStatusBinding.RequestAuthorizationTracking();
            }
#endif
        }
    }
}