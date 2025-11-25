using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField] private AudioClip starClip;
    [SerializeField] private AudioClip BigStarClip;
    [SerializeField] private AudioClip obstacleClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip MissileClip;

    [SerializeField]private AudioClip ButtonClip;

    private AudioSource sfxSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        sfxSource =gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.7f;
    }
    //播放音效
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    public void StarSfx()//收集到星星
    {
        PlaySFX(starClip);
    }
    public void BigStarSfx()//收集大猩猩
    {
        PlaySFX(BigStarClip);
    }
    public void obstacleSfx()//碰到障碍的音效
    {
        PlaySFX(obstacleClip);
    }
    public void JumpSfx()//跳跃音效
    {
        PlaySFX(jumpClip);
    }
    public void ButtonSfx()
    {
        PlaySFX(ButtonClip);
    }
    public void MissileSfx()
    {
        PlaySFX(MissileClip);
    }
}
