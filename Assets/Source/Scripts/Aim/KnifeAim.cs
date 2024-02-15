using System;
using Source.Scripts.Knifes;
using UnityEngine;

namespace Source.Scripts.Aim
{
    public abstract class KnifeAim : MonoBehaviour, IKnifeAim
    {
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Knife"))
            {
                GetKnife(other.GetComponent<Knife>());
            }
        }

        public abstract void GetKnife(Knife knife);
    }
}