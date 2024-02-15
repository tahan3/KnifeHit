using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Pool
{
    public class ComponentsPool<T> : IPool<T> where T : Component
    {
        private T _prefab;
        private Queue<T> _items;
        private Transform _parent;
        
        private DiContainer _container;
        
        public ComponentsPool(DiContainer container, T prefab, int initialPoolSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _container = container;

            InitPool(initialPoolSize);
        }

        private void InitPool(int size)
        {
            _items = new Queue<T>(size);
            
            for (int i = 0; i < size; i++)
            {
                var item = _container.InstantiatePrefab(_prefab, _parent);
                item.SetActive(false);
                _items.Enqueue(item.GetComponent<T>());
            }
        }
        
        public T GetItem()
        {
            var item = _items.Dequeue();

            if (item.gameObject.activeSelf)
            {
                _items.Enqueue(item);
                item = _container.InstantiatePrefab(_prefab, _parent).GetComponent<T>();
                item.transform.SetParent(_parent);
            }
            
            _items.Enqueue(item);

            return item;
        }
    }
}