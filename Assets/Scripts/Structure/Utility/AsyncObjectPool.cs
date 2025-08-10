using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Structure.Utility;

public class AsyncObjectPool<T> where T : Component
{
    private Queue<T> Pool { get; }
    private T Prefab { get; }
    private int Capacity { get; }

    public AsyncObjectPool(T prefab, int capacity)
    {
        Prefab = prefab;
        Pool = new Queue<T>(capacity);
        Capacity = capacity;
    }

    public async UniTask InitializeAsync()
    {
        var poolObject = await Object.InstantiateAsync(Prefab, Capacity).ToUniTask();

        for (int i = 0; i < poolObject.Length; i++)
        {
            poolObject[i].gameObject.SetActive(false);
            Pool.Enqueue(poolObject[i]);
        }
    }

    public T Get()
    {
        if (Pool.TryDequeue(out var pooledObject))
        {
            pooledObject!.gameObject.SetActive(true);
            return pooledObject;
        }

        throw new Exception("Pool Empty Exception");
    }

    public void Return(T obj)
    {
        if (obj == null)
        {
            return;
        }

        obj.gameObject.SetActive(false);
        Pool.Enqueue(obj);
    }
}