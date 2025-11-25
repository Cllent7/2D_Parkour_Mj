using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //单例
    public static PlayerController Instance { get; private set; }

    [Header("移动")]
    [SerializeField] float speedMove;
    [SerializeField] float speedJump ;
    [SerializeField] float slideSpeed; //滑行速度倍率

    [Header("地面检测")]
    [SerializeField] Transform checkPoint;
    [SerializeField] float checkRange = 0.1f;
    [SerializeField] LayerMask groundLayer;

    // 状态变量
    private bool isGround;
    private bool isSlides;
    private bool isJump;
    private bool isGameOver = false;
    private int jumpCount = 0;

    private Vector2 boxSize;

    private Vector3 initialPosition; // 存储玩家初始位置

    // 组件引用
    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D triggerCol2D;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            initialPosition= transform.position;
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        triggerCol2D = GetComponent<CapsuleCollider2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxSize = boxCollider2D.size;
    }

    private void Update()
    {
        IsGround();
        RunRight();
        Anim();
    }
    private void RunRight()//一直往右边跑
    {
        if (isGameOver) return;//游戏结束时停止运动
        float currentSpeed = isSlides ? speedMove * slideSpeed : speedMove;
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }
    private void IsGround()
    {
        bool wasGrounded = isGround;
        isGround = Physics2D.OverlapCircle(checkPoint.position, checkRange, groundLayer);
        if (!wasGrounded && isGround)
        {
            isJump = false;
            jumpCount = 0;
        }
    }
    private void Anim()
    {
        animator.SetBool("Jump", isJump);
        animator.SetBool("Slide", isSlides);
    }
    private void OnEnable()
    {
        InputManager.Instance.OnJump += Jump;
        InputManager.Instance.OnSlideStart += EnterSlides;
        InputManager.Instance.OnSlideEnd += ExitSlides;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        InputManager.Instance.OnJump -= Jump;
        InputManager.Instance.OnSlideStart -= EnterSlides;
        InputManager.Instance.OnSlideEnd -= ExitSlides;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        initialPosition = transform.position; // 场景加载后更新初始位置
    }
    public void Jump()
    {
        if (isGround || jumpCount < 2)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.velocity += Vector2.up * speedJump;
            jumpCount++;
            AudioManager.instance.JumpSfx();
        }
    }
    public void StopMovement()
    {
        isGameOver = true;
        rb.velocity = Vector2.zero;
    }
    private void EnterSlides()
    {
        isSlides = true;
         boxCollider2D.size = new Vector2(boxCollider2D.size.x, boxCollider2D.size.y * 0.5f);
    }

    private void ExitSlides()
    {
        isSlides = false;
         boxCollider2D.size = boxSize;
    }

    protected virtual void OnDrawGizmos()
    {
        if (checkPoint != null)
        {
            Gizmos.color = isGround ? Color.green : Color.red;
            Gizmos.DrawWireSphere(checkPoint.position, checkRange);
        }
    }
    public void ResetPosition()//重置全部信息
    {
        transform.position = initialPosition;
        isGameOver = false;
        isSlides = false;   
        isJump = false;
        jumpCount = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        // 重置碰撞体大小
        boxCollider2D.size = boxSize;
        // 重置动画
        animator.SetBool("Jump", false);
        animator.SetBool("Slide", false);
    }
}