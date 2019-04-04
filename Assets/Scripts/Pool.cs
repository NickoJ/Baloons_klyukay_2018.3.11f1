using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Klyukay.Balloons
{

    public class MonoPool<T> where T : MonoBehaviour
    {

        private readonly Queue<T> _queue = new Queue<T>();
        private readonly T _prefab;
        
        public MonoPool(T prefab, int initSize)
        {
            if (prefab == null) throw new NullReferenceException();
            
            _prefab = prefab;
            for (int i = 0; i < initSize; i++)
            {
                _queue.Enqueue(CreateNewItem());
            }
        }

        public T Take() => _queue.Count != 0 ? _queue.Dequeue() : CreateNewItem();

        public void Release(T item)
        {
            if (item == null) throw new NullReferenceException();
            ResetItem(item);
            _queue.Enqueue(item);
        }

        private T CreateNewItem()
        {
            var item = Object.Instantiate(_prefab);
            ResetItem(item);
            return item;
        }

        private void ResetItem(T item)
        {
            item.gameObject.SetActive(false);
        }
        
    }

}