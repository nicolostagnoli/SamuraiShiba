using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAfterImagePool : MonoBehaviour
{
    public GameObject afterImagePrefab;

    private Queue<GameObject> avialableObjects = new Queue<GameObject>();
    
    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        avialableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (avialableObjects.Count == 0)
        {
            GrowPool();
        }

        var instance = avialableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
