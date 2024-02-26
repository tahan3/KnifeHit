using Facebook.Unity;
using UnityEngine;

public class FacebookInitialiser : MonoBehaviour
{
    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            print("SDK correctly initializated: " + FB.FacebookImpl.SDKUserAgent);

            FB.ActivateApp();
        }
        else
        {
            print("Failed to Initialize the Facebook SDK");
        }
    }
}
