using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public event Action OnJump;
    public event Action OnSlideStart;
    public event Action OnSlideEnd;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
            
        }
    }
    public void OnjumpButtonClicked()
    {
        Debug.Log("滑动按钮按下！");
        OnJump?.Invoke();
    }
    public void OnSlideButtonDown()
    {
        Debug.Log("滑动按钮按下！");
        OnSlideStart?.Invoke();
    }
    public void OnSlideButtonUp()
    { 
        OnSlideEnd?.Invoke();
    }
}
