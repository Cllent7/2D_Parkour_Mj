using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public static RankManager instance { get; private set; }
    [SerializeField] private MyData gameData;
    private const string KEY_RECET_DISTANCES = "RancentDistance";
    private const string KEY_FARTHEST_DISTANCE = "FarthestDistance";
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //处理新的距离记录
    public void AddRecentDistances(float distance)
    {
        if (gameData.RecentDistances.Count >= 3)
        {
            gameData.RecentDistances.RemoveAt(0);//移除最早记录
        }
        gameData.RecentDistances.Add(distance);
        UpdateFarthestDistance();
        SaveData();
    }
    //记录近三次最远的距离
    private void UpdateFarthestDistance()
    {
        float max = 0;
        foreach (var dist in gameData.RecentDistances)
        {
            if(dist > max)max = dist;

        }
        gameData.FarthestDistance = max;
        SaveData();
    }
    private void SaveData()
    {
        //保存最近三次记录
        PlayerPrefs.DeleteKey(KEY_RECET_DISTANCES);
        for (int i = 0; i < gameData.RecentDistances.Count; i++)
        {
            PlayerPrefs.SetFloat($"{KEY_RECET_DISTANCES}_{i}", gameData.RecentDistances[i]);

        }
        PlayerPrefs.SetInt($"{KEY_RECET_DISTANCES}_Count", gameData.RecentDistances.Count);
        PlayerPrefs.SetFloat(KEY_FARTHEST_DISTANCE, gameData.FarthestDistance);
        PlayerPrefs.SetInt("TotalStars", gameData.TotalStars);
        PlayerPrefs.Save();
    }
    private void LoadData()
    {
        gameData.RecentDistances.Clear();
        int count =PlayerPrefs.GetInt($"{KEY_RECET_DISTANCES}_Count", 0);
        for (int i = 0; i < count; i++)
        {
            float distance = PlayerPrefs.GetFloat($"{KEY_RECET_DISTANCES}_{i}", 0);
            gameData.RecentDistances.Add(distance);

        }
        // 加载最远记录
        gameData.FarthestDistance = PlayerPrefs.GetFloat(KEY_FARTHEST_DISTANCE, 0);
        gameData.TotalStars = PlayerPrefs.GetInt("TotalStars", 0);
    }

    //获取排行榜数据
    public List<float> GetRecentDistances()
    {
        return new List<float>(gameData.RecentDistances  );
    }
    public float GetFarthestDistance()
    {
        return gameData.FarthestDistance;
    }
    public float TotalStars()
    {
        return gameData.TotalStars;
    }
}
