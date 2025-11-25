using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    public static RankUI Instance { get; private set; }
    
    [SerializeField] private Text recentRecord1Text; // 最近第1次
    [SerializeField] private Text recentRecord2Text; // 最近第2次
    [SerializeField] private Text recentRecord3Text; // 最近第3次
    [SerializeField] private Text farthestText; // 最远记录]
    [SerializeField] private Text totalStarsText; 
    [SerializeField] private MyData gameData; 

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
    }
    public void UpdateRankText()
    { 
        List<float> recentDistances = RankManager.instance.GetRecentDistances();
        // 显示最近3条记录（不足3条则显示空）
        recentRecord1Text.text = recentDistances.Count > 0 ? recentDistances[0].ToString("F1") : "0";
        recentRecord2Text.text = recentDistances.Count > 1 ? recentDistances[1].ToString("F1") : "0";
        recentRecord3Text.text = recentDistances.Count > 2 ? recentDistances[2].ToString("F1") : "0";

        farthestText.text = RankManager.instance.GetFarthestDistance().ToString("F1");
        totalStarsText.text=RankManager.instance.TotalStars().ToString();
    }
}
