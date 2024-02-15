using System;

namespace Source.Scripts.Spawn
{
    public interface ISpawner<out T>
    {
        public T Spawn();
        public event Action<T> OnSpawned;
    }
}