using UnityEngine;

namespace Source.Scripts.UI.ProgressBar
{
    public abstract class ProgressBar<T> : MonoBehaviour, IProgressBar<T>
    {
        public abstract void SetProgress(T fillValue);
    }
}