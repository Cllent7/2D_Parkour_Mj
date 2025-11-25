using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DistanceManager : MonoBehaviour
{

    public static DistanceManager Instance { get; private set; }
    public event Action<float> OnDistanceUpdated;
    public float CurrentDistance { get; private set; }

    [Tooltip("玩家Transform组件，若未赋值将自动查找标签为Player的对象")]
    public Transform PlayerTransform;

    private float _startX;

    // 确保单例唯一性
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 跨场景保留
            InitializePlayerReference(); // 初始化玩家引用
        }
        else
        {
            Destroy(gameObject); // 销毁重复实例
        }
    }

    private void Start()
    {
        ResetDistance(); // 确保初始距离正确
    }

    private void InitializePlayerReference()
    {
        if (PlayerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerTransform = player.transform;
            }

        }
    }

    // 每帧更新距离
    private void Update()
    {
        // 安全检查：游戏结束或玩家引用丢失时不更新
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (PlayerTransform == null)
        {
            InitializePlayerReference(); // 尝试重新获取玩家引用
            return;
        }


        CurrentDistance = Mathf.Max(0, PlayerTransform.position.x - _startX);
        OnDistanceUpdated?.Invoke(CurrentDistance);
    }
    public void ResetDistance()
    {
        if (PlayerTransform != null)
        {
            _startX = PlayerTransform.position.x;
            CurrentDistance = 0;
            OnDistanceUpdated?.Invoke(CurrentDistance); // 触发初始值更新

        }

    }

    // 场景加载时重新检查玩家引用
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializePlayerReference();
        ResetDistance();
    }

    // 注册场景加载事件
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 取消注册场景加载事件
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}