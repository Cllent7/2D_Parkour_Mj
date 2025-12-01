using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_2_UIManager:MonoBehaviour
{
    public static Level_2_UIManager Instance { get; private set; }

    [Header("按键")]
    [SerializeField] private Button RestartBtn;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button continueBtn;
    [Header("面板")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject DeadPanel;
    [SerializeField] private MyData gameData;

    // Level2特有的UI元素
    [Header("文本")]
    [SerializeField] private Text scoreText;
    [SerializeField] private Text distanceText;
    [SerializeField] private Text starTextDead;
    [SerializeField] private Text distanceTextDead;
    // 引用公共模块
    private UICommonModule commonModule;
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
        commonModule = new UICommonModule(scoreText, pauseBtn, continueBtn, pausePanel, DeadPanel, gameData);

        RestartBtn.onClick.AddListener(OnRestart);
    }
    private void OnEnable()
    {
        DistanceManager.Instance.OnDistanceUpdated += UIUpdated;    
        GameEvents.OnPlayerDied += OnPlayerDied;
    }

    public void UIUpdated(float distance)//更新ui
    {
        commonModule.UpdateScoreText();
        distanceText.text = $"当前距离: {distance:F1}米";
    }
    public void OnRestart()
    {
        GameManager.Instance.ResetGame();
        DistanceManager.Instance.ResetDistance();
        float currentDistance = DistanceManager.Instance.CurrentDistance;
        if (currentDistance > gameData.FarthestDistance)
        {
            gameData.FarthestDistance = currentDistance;
        }
        DeadPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
    public void OnPlayerDied()
    {
        DeadPanel.SetActive(true);
        Time.timeScale = 0;
        RankManager.instance.AddRecentDistances(DistanceManager.Instance.CurrentDistance);
        starTextDead.text = gameData.currentStar.ToString();
        distanceTextDead.text =$"距离: { DistanceManager.Instance.CurrentDistance:F1}米";


    }
    public void ToMain()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        SceneManager.LoadScene("Statr_Scene");
    }
    private void OnDestroy()
    {
        // 移除事件监听
        DistanceManager.Instance.OnDistanceUpdated -= UIUpdated;
        GameEvents.OnPlayerDied -= OnPlayerDied;
    }
}
