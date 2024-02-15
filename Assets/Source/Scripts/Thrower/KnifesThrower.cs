using Source.Scripts.Knifes;
using Source.Scripts.Pool;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Thrower
{
    public class KnifesThrower : IThrower<Knife>
    {
        private readonly float _throwForce;

        public KnifesThrower(float throwForce)
        {
            _throwForce = throwForce;
        }
        
        public void Throw(Knife knife)
        {
            knife.rigidbody.velocity = knife.transform.forward * _throwForce;
        }
    }
}