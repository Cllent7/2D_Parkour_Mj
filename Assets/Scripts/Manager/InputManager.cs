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
            
        }
        else
        {
            Destroy(gameObject);
            
        }
    }
    public void OnjumpButtonClicked()
    {
        OnJump?.Invoke();
    }
    public void OnSlideButtonDown()
    {
        OnSlideStart?.Invoke();
    }
    public void OnSlideButtonUp()
    { 
        OnSlideEnd?.Invoke();
    }
}
