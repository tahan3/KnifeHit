using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Data
{
    public abstract class KeyValueStorage<TKey, TValue> : ScriptableObject
    {
        [SerializedDictionary("Key", "Value")] 
        [SerializeField] protected SerializedDictionary<TKey, TValue> items;

        public bool TryGetValue(TKey key, out TValue value)
        {
            return items.TryGetValue(key, out value);
        }

        public List<TKey> GetKeys()
        {
            return items.Keys.ToList();
        }
    }
}