using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_1_UIManager :MonoBehaviour
{
    public static Level_1_UIManager Instance { get; private set; }
    // 公共模块
    [SerializeField] private Text scoreText;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private MyData gameData;
    //关卡1本身的
    [SerializeField] private GameObject SuccessPanle;//成功到达50分的面板
    [SerializeField] private Button nextLevle;

    [SerializeField] private UICommonModule commomModule;
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

        commomModule = new UICommonModule(scoreText, pauseBtn, continueBtn, pausePanel, DeadPanel, gameData);
        //注册事件
        GameEvents.OnScoreUpdated += OnScoreUpdated;
        GameEvents.OnPlayerDied += OnPlayerDied;

        nextLevle.onClick.AddListener(ToLevel2);
    }
    private void OnScoreUpdated(int newScore)
    {
        //ui更新
        commomModule.UpdateScoreText();
        //游戏成功（第一关）
        if (newScore == 50)
        {
            Time.timeScale = 0;
            SuccessPanle.SetActive(true);
        }
    }
    public void ToLevel2()
    {
        AudioManager.instance.ButtonSfx();
        Time.timeScale = 1;
        SceneManager.LoadScene("Level2");
    }
    public void OnPlayerDied()
    {
        DeadPanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void OnRestart()
    {
        GameManager.Instance.ResetGame();
        DeadPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void ToMain()
    {
        AudioManager.instance.ButtonSfx();
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene("Statr_Scene");
    }
    public void OnDestroy()
    {
        GameEvents.OnScoreUpdated -= OnScoreUpdated;
        GameEvents.OnPlayerDied -= OnPlayerDied;
    }
}
