using UnityEngine;

namespace Source.Scripts.Connection
{
    public class InternetConnection
    {
        public static bool ConnectionCheck()
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
}