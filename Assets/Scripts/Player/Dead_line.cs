using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_line : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySound(SoundEffectType.obstacleClip);
            GameManager.Instance.GameOver();
        }
    }
}
