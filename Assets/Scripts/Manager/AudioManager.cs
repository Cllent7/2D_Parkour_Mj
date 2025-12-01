using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SoundEffectType
{
    starClip,
    BigStarClip,
    obstacleClip,
    jumpClip,
    MissileClip,
    ButtonClip
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [System.Serializable]
    public class SoundEffectPair
    {
        public SoundEffectType Type;
        public AudioClip Clip;
    }
    [SerializeField]private List<SoundEffectPair>soundEffects = new List<SoundEffectPair>();
    private Dictionary<SoundEffectType,AudioClip>soundDict = new Dictionary<SoundEffectType,AudioClip>();
    private AudioSource sfxSource;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        sfxSource =gameObject.AddComponent<AudioSource>();
        sfxSource.volume = 0.7f;

        foreach (var pair in soundEffects)
        {
            if (!soundDict.ContainsKey(pair.Type))
                soundDict.Add(pair.Type,pair.Clip);
        }
    }
    public void PlaySound(SoundEffectType type)
    {
        if (soundDict.TryGetValue(type, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
