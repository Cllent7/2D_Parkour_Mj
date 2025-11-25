using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollerMap : MonoBehaviour
{
    [SerializeField] RawImage bg;
    [SerializeField] float x;
    private float y = 0f;
    private Vector2 initialUVOffset;
    private void Start()
    {
        // 记录初始UV偏移
        initialUVOffset = bg.uvRect.position;
    }
    private void Update()
    {
        if (!GameManager.Instance.IsGameOver)
        { 
            
            Vector2 scrollOffset =new Vector2(x, y)*Time.deltaTime;
            bg.uvRect = new Rect(bg.uvRect.position + scrollOffset, bg.uvRect.size);
        }
        
    }
    private void OnEnable()
    {
        GameEvents.OnGameReset += ResetBackground;
    }

    private void OnDisable()
    {
        GameEvents.OnGameReset -= ResetBackground;
    }

    private void ResetBackground()
    {
        // 重置背景到初始位置
        bg.uvRect = new Rect(initialUVOffset, bg.uvRect.size);
    }

}
