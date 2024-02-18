using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Pool
{
    public class ComponentsPool<T> : IPool<T> where T : Component
    {
        private T _prefab;
        private Queue<T> _freeItems;
        private Queue<T> _inUseItems;
        private Transform _parent;
        
        private DiContainer _container;
        private int _additiveSize;
        
        public ComponentsPool(DiContainer container, T prefab, int initialPoolSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;
            _container = container;
            _additiveSize = initialPoolSize;

            InitPool();
        }

        private void InitPool()
        {
            _freeItems = new Queue<T>(_additiveSize);
            _inUseItems = new Queue<T>(_additiveSize);

            ExtendPool(_additiveSize);
        }
        
        public T GetItem()
        {
            if (_freeItems.Count <= 0)
            {
                GetFreeFromInUse();

                if (_freeItems.Count < _additiveSize)
                {
                    ExtendPool(_additiveSize - _freeItems.Count);
                }
            }
            
            T item = _freeItems.Dequeue();
            _inUseItems.Enqueue(item);
            
            return item;
        }

        protected virtual T CreateItem()
        {
            return _container.InstantiatePrefab(_prefab, _parent).GetComponent<T>();
        }
        
        protected virtual T OnItemCreated(T item)
        {
            item.gameObject.SetActive(false);
            return item;
        }

        protected virtual void ExtendPool(int size)
        {
            for (int i = 0; i < size; i++)
            {
                var item = OnItemCreated(CreateItem());
                _freeItems.Enqueue(item);
            }
        }
        
        protected virtual void GetFreeFromInUse()
        {
            for (var i = 0; i < _inUseItems.Count; i++)
            {
                var item = _inUseItems.Dequeue();

                if (IsFreeItem(item))
                {
                    _freeItems.Enqueue(item);
                }
            }
        }

        protected virtual bool IsFreeItem(T item)
        {
            return !item.gameObject.activeSelf;
        }
    }
}