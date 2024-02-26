using System;
using System.Collections;
using Newtonsoft.Json;
using Source.Scripts.DailyReward;
using Source.Scripts.Data.Screen;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.DailyReward
{
    public class DailyRewardOpener : MonoBehaviour
    {
        private WindowsHandler _windowsHandler;

        [Inject]
        public void Construct(WindowsHandler windowsHandler)
        {
            _windowsHandler = windowsHandler;
        }
        
        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);


        }
    }
}