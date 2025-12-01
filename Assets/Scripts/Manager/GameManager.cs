using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private MyData gameDate;

    private bool isGameOver;
    public bool IsGameOver => isGameOver;
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
        gameDate.currentStar = 0;
        isGameOver = false;
        Time.timeScale = 1;
        GameEvents.TriggerGameReset();
    }
    public void ResetGame()
    {

        isGameOver =false;
        Time.timeScale = 1;
        gameDate.currentStar = 0;
        PlayerController.Instance.ResetPosition();
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        GameEvents.TriggerGameReset();
        //更新文本
        GameEvents.TriggerScoreUpdated(gameDate.currentStar);
    }

    public  void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        PlayerController.Instance.StopMovement();
        GameEvents.TriggerPlayerDied();
    }
    
}
