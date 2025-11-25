// 新建Star.cs脚本挂在星星预制体上
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.obstacleSfx();
            GameEvents.TriggerPlayerDied();
        }
    }
}
