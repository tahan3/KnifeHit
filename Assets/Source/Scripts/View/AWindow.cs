using UnityEngine;

namespace Source.Scripts.View
{
    public abstract class AWindow : MonoBehaviour, IView
    {
        public abstract void Open();

        public abstract void Close();

        public abstract bool IsActive();
    }
}