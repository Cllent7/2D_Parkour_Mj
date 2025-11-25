using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileObstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float triggerDistance = 5f;//触发飞行的距离
    private Vector3 moveDirection = Vector3.left;
    private Transform playerTransform;
    private bool isFlying = false;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }
    private void Update()
    {
        if (isFlying)//如果导弹在飞行
        {
            transform.Translate(moveDirection*moveSpeed*Time.deltaTime);
        }
        else if (playerTransform != null)
        {
            float distance = Mathf.Abs(playerTransform.position.x - transform.position.x);
            if (distance <= triggerDistance)
            {
                
                isFlying = true;
                AudioManager.instance.MissileSfx();
                Debug.Log("，起飞！！！！！");
            
            }
        }
            

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.obstacleSfx();
            GameManager.Instance.GameOver();
        }
    }
}
