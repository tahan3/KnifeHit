using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Data
{
    public abstract class KeyValueStorage<TKey, TValue> : ScriptableObject
    {
        [SerializedDictionary("Key", "Value")] 
        [SerializeField] protected SerializedDictionary<TKey, TValue> items;

        public abstract TValue GetValue(TKey key);
    }
}