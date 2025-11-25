using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMapGenerator : MonoBehaviour
{
    public Transform Player;
    public int initilSegments = 3;
    private List<GameObject>activeSegments = new List<GameObject>();
    private float lastSegmentEndX;

    private void Start()
    {
        //游戏开始加载地图
        for (int i = 0; i < initilSegments; i++)
        {
            SpawnNewSegment();
        }
    }
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;
        if (Player == null)
            return;
        if (Player.position.x > lastSegmentEndX - 30f)
        {
            SpawnNewSegment();
            RemoveOldSegment();
        }
    }
    private void SpawnNewSegment()
    {
        //随机选一片段
        GameObject newSegment = MapPoolManager.Instance.GetRandomSegment();
        //获取这个地图段的信息 
        MapSegment segmentData = newSegment.GetComponent<MapSegment>();
        //新生成地图段的位置
        float spawnX = activeSegments.Count == 0 ? 0 : lastSegmentEndX;
        newSegment.transform.position = new Vector3(spawnX, 0, 0);
        activeSegments.Add(newSegment);
        lastSegmentEndX = spawnX+segmentData.Length;
    }
    private void RemoveOldSegment()
    { 
        if (activeSegments.Count > initilSegments+1)
        {
            GameObject oldSegment =activeSegments[0];
            activeSegments.RemoveAt(0);
            oldSegment.SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnGameReset += ResetSegments;
    }
    private void OnDestroy()
    {
        GameEvents.OnGameReset -= ResetSegments;
    }
    public void ResetSegments()
    {
       
        foreach (var segment in activeSegments)
        {
            segment.SetActive(false);
        }
        activeSegments.Clear();
        lastSegmentEndX = 0;
        for (int i = 0; i < initilSegments; i++)
        {
            SpawnNewSegment();
        }
    }
}
