using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoolManager : MonoBehaviour
{
    public static MapPoolManager Instance { get; private set; }
    public List<GameObject> segmentPrefabs;
    //对象池容量
    public int poolSize = 5;
    private List<Queue<GameObject>> segmentPools =new List<Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        { 
            Destroy(gameObject);
        }
        CreatePool();
    }
    void CreatePool()
    {
        foreach (var prefab in segmentPrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject segment = Instantiate(prefab);
                segment.SetActive(false);
                pool.Enqueue(segment);
            }
            segmentPools.Add(pool);
        }
    }
    //获得一个随机的地图段
    public GameObject GetRandomSegment()
    {
        int randomIndex;
        do
        {
            randomIndex = UnityEngine.Random.Range(0, segmentPrefabs.Count); 
        } while (IsSameAsLastSegment(randomIndex)); 

        var pool = segmentPools[randomIndex];
        GameObject segmentToSpawn = pool.Dequeue();
        
        segmentToSpawn.SetActive(true);
        pool.Enqueue(segmentToSpawn);
        return segmentToSpawn;

    }
    private int lastSegmentIndex = -1;//记录和检查上一次使用的地图段
    private bool IsSameAsLastSegment(int index)
    {
        if (index == lastSegmentIndex)
        { 
            return true;
        }
        lastSegmentIndex = index;
        return false;
    }
}
