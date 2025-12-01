using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager instance { get; private set; }
    //监听按钮
    [SerializeField] private Button startLevel1Btn;
    [SerializeField] private Button startLevel2Btn;
    [SerializeField] private Button exitGameBtn;
    [SerializeField] private Button BackMianBtn;

    [SerializeField] private Button rankingListBtn;//排行榜打开按钮
    [SerializeField] protected GameObject rankingPanel;
    [SerializeField] protected GameObject Start_Panel;

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
        //绑定他们
        startLevel1Btn.onClick.AddListener(OnStartLevel);
        startLevel2Btn.onClick.AddListener(OnStartLevel2);
        exitGameBtn.onClick.AddListener(OnExitGame);
        rankingListBtn.onClick.AddListener(OnrankingList);
        BackMianBtn.onClick.AddListener(OnBackMian);
    }
    //切换到关卡一

    public void OnStartLevel()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        SceneManager.LoadScene("Level1");
    }
    private void OnStartLevel2()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        SceneManager.LoadScene("Level2");
    }
    private void OnrankingList()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        rankingPanel.SetActive(true);
        Start_Panel.SetActive(false);
        RankUI.Instance.UpdateRankText();
    }
    private void OnBackMian()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);

        rankingPanel.SetActive(false);
        Start_Panel.SetActive(true);
    }
    private void OnExitGame()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        Application.Quit();
        Debug.Log("退出游戏");
    }
}
