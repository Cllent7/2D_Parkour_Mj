using UnityEngine;
using UnityEngine.UI;

public class UICommonModule
{
    // 公共UI控件
    public Text scoreText;
    public Button pauseBtn;
    public Button continueBtn;
    public GameObject pausePanel;
    public GameObject DeadPanel;
    public MyData gameData;

    public UICommonModule(Text scoreText, Button pauseBtn, Button continueBtn,
                         GameObject pausePanel, GameObject DeadPanel, MyData gameData)
    {
        this.scoreText = scoreText;
        this.pauseBtn = pauseBtn;
        this.continueBtn = continueBtn;
        this.pausePanel = pausePanel;
        this.DeadPanel = DeadPanel;
        this.gameData = gameData;

        // 绑定公共事件
        pauseBtn.onClick.AddListener(OnPause);
        continueBtn.onClick.AddListener(OnContinue);
    }

    // 暂停
    public void OnPause()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    // 继续
    public void OnContinue()
    {
        AudioManager.instance.PlaySound(SoundEffectType.ButtonClip);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    //更新星星
    public void UpdateScoreText()
    {
        scoreText.text = gameData.currentStar.ToString();
    }
}