using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//fileName默认文件名，menuName在菜单中的名字，order表示位置顺序
[CreateAssetMenu(fileName = "Mj_Data", menuName = "ScriptableObject")]
public class MyData : ScriptableObject
{
    public int currentStar;
    [SerializeField] private int totalStars; 
    [SerializeField] List<float> recentDistances = new List<float>(); // 最近三次距离
    [SerializeField]  float farthestDistance; // 最远记录
    public List<float> RecentDistances
    {
        get => recentDistances;
        set => recentDistances = value;
    }
     
    public float FarthestDistance
    {
        get => farthestDistance;
        set => farthestDistance = value;
    }
    public int TotalStars
    {
        get => totalStars;
        set => totalStars = value;
    }


}