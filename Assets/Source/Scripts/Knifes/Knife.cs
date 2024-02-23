using Source.Scripts.Aim;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Knifes
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Knife : MonoBehaviour
    {
        public Rigidbody rigidbody;
        public Collider collider;
        public KnifeState state;

        [Inject] private DiContainer _container;

        private KnifeEjector _ejector;
        
        //TEST
        public void SetIdle()
        {
            if (state == KnifeState.Idle) return;
            
            state = KnifeState.Idle;
            rigidbody.velocity = Vector3.zero;
            collider.isTrigger = true;
            _ejector = gameObject.AddComponent<KnifeEjector>();
            _container.Inject(_ejector);
        }

        public void SetActive(bool mode)
        {
            collider.enabled = mode;
            rigidbody.detectCollisions = mode;
        }
        
        public void SetFree()
        {
            state = KnifeState.Free;
            collider.isTrigger = false;
            collider.enabled = true;
            
            if (_ejector)
            {
                Destroy(_ejector);
            }
        }
    }
}