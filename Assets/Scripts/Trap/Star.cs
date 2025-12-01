
using UnityEngine;
using UnityEngine.SceneManagement;

public class Star : MonoBehaviour
{
    [SerializeField] int scoreValue ;
    [SerializeField] MyData gameData;
    [SerializeField] private AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameData.currentStar += scoreValue;
            gameData.TotalStars += scoreValue;  // 累计总金币
            if (scoreValue >= 5)
            {
                AudioManager.instance.PlaySound(SoundEffectType.BigStarClip);
            }
            else
            { AudioManager.instance.PlaySound(SoundEffectType.starClip); }
            //更新ui显示
            GameEvents.TriggerScoreUpdated(gameData.currentStar);
            gameObject.SetActive(false);
        }
    }

}