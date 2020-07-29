using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using QuantumTek.QuantumUI;
using Lean.Gui;

public class UI_Control : Singleton<UI_Control>
{
    public Text MoneyText;
    [SerializeField] private Image dangerImage;
    [SerializeField] private float timeToRed = 1f;
    [SerializeField] private float levelToRed = 0.5f;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider enemySlider;
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private QUI_SceneTransition qUI_SceneTransition;
    [SerializeField] private GameObject describePanel;
    [SerializeField] private TMPro.TextMeshProUGUI describePanelText;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private QUI_OptionList cameras;

    [SerializeField] private LeanToggle musicToggle;
    [SerializeField] private LeanToggle smartAcceleration;
    [SerializeField] private Slider gSenseSlider;

    [SerializeField] private GameObject looseMenu;
    [SerializeField] private GameObject winMenu;

    public Button getMoreHeartsButton;

    private Color startColor;

    [SerializeField] private Slider sliderHerat;

    private SoundControl soundControl;

    [SerializeField] private Text ptsField;
    public bool isFreeRun = false;

    private GameBuilder gameBuilder;


    // Start is called before the first frame update GameStats.shootAviable

    [SerializeField] private Button shootButton;
    [SerializeField] private Button boostButton;


    private void Awake()
    {
        gameBuilder = GameBuilder.Instance;
        soundControl = SoundControl.Instance;
    }
    void Start()
    {
       
        GameStats.playClick = false;
        startColor = dangerImage.color;
        healthSlider.maxValue = gameBuilder.GetMaxHealth();
        RenewtHealthUI();
        RenewMoneyUI();

        musicToggle.Set(GameStats.useMusic);
        smartAcceleration.Set(GameStats.useSmartAcceleration);
        gSenseSlider.value = GameStats.gSensCoef;
        gSenseSlider.interactable = GameStats.useSmartAcceleration;
        GameStats.playClick = true;


    }

    public void ShowShootButton(bool showB)
    {
        shootButton.interactable = showB;
    }

    public void ShowBoostButton(bool showB)
    {
        boostButton.interactable = showB;
    }

    public void RenewMoneyUI()
    {
        MoneyText.text = GameStats.GetСurrency().ToString();
        
        if (GameStats.GetСurrency() <= 0)
        {
            ShowBoostButton(false);
        }
        else
        {
            ShowBoostButton(true) ;
        }

    }
    public void RenewtHealthUI()
    {
        healthSlider.value = GameStats.GetHealth();
        healthSlider.maxValue = gameBuilder.GetMaxHealth();
        if (isFreeRun)
        {
            ptsField.text = GameStats.freeRunPts.ToString();
        }
    }

    public void RenewEnemyUI()
    {
        enemySlider.value = GameStats.GetEnemyHealth();
        enemySlider.maxValue = gameBuilder.GetMaxEnemyHealth();
        
    }


    public void MakeRedScreen()
    {
        StartCoroutine(MakeRedScreenCorut(timeToRed, levelToRed));
        soundControl.Damage();
    }
    public void MakeRedScreen(float timerToR, float levelToR)
    {
        soundControl.Damage();
        StartCoroutine(MakeRedScreenCorut(timerToR, levelToR));
    }

    private IEnumerator MakeRedScreenCorut(float timerToR, float levelToR)
    {
        Color newColor = new Color(dangerImage.color.r, dangerImage.color.g, dangerImage.color.b, levelToR);
        float t = 0.0f;

        while (dangerImage.color.a < levelToR)
        {
            dangerImage.color = Color.Lerp(startColor, newColor, t);
            t += Time.deltaTime * 2 / timerToR;
            yield return new WaitForEndOfFrame();
        }

        t = 0.0f;
        while (dangerImage.color.a > startColor.a)
        {
            dangerImage.color = Color.Lerp(newColor, startColor, t);
            t += Time.deltaTime *2 / timerToR;
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void GoPauseMenu()
    {
        GameBuilder.Instance.GamePause();
        pauseMenuCanvas.SetActive(true);
        soundControl.Click();
    }

    public void OffPauseMenu()
    {
        pauseMenuCanvas.SetActive(false);
        GameBuilder.Instance.GameResume();
        soundControl.Click();
    }

    public void GoToStartScreen()
    {
        soundControl.Click();
        OffPauseMenu();
        qUI_SceneTransition.LoadScene("StartScreen");
    }

    public void ShowDescribePanel(string textToShow = "Complete level tp win!")
    {

        GameBuilder.Instance.GamePause();
        describePanelText.text = textToShow;
        describePanel.SetActive(true);
       
    }

    public void OffDescribePanel()
    {
        describePanel.SetActive(false);
        GameBuilder.Instance.GameResume();
        soundControl.Click();
    }

    public void ShowOptionsPanel()
    {
        cameras.SetOption(GameStats.GetCurrentCamera());
        optionsPanel.SetActive(true);
        soundControl.Click();
    }

    public void OffOptionsPanel()
    {
        optionsPanel.SetActive(false);
        soundControl.Click();
        DataControl.SaveData();
    }

    public void SetCamerView()
    {
        GameBuilder.Instance.SetCamera(cameras.optionIndex);
        soundControl.Click();
    }

    public void MusicOn()
    {
        GameStats.useMusic = true;
        soundControl.Music();
    }

    public void MusicOff()
    {
        GameStats.useMusic = false;
        soundControl.StopMusic();
    }


    public void SmartRotateOn()
    {
        GameStats.useSmartAcceleration = true;
        gSenseSlider.interactable = true;
        soundControl.Click();
    }

    public void SmartRotateOff()
    {
        GameStats.useSmartAcceleration = false;
        gSenseSlider.interactable = false;
        soundControl.Click();
    }

    public void SetGSense()
    {
        GameStats.gSensCoef = gSenseSlider.value;
        soundControl.Click();
    }
    
    public void ShowLooseMenu()
    {
        GameBuilder.Instance.GamePause();
        looseMenu.SetActive(true);
        soundControl.Loose();
    }

    public void ShowWinMenu()
    {
        GameBuilder.Instance.GamePause();
        winMenu.SetActive(true);
        soundControl.Win();
    }

    public void RetryLevel(bool fromStart = false)
    {
        looseMenu.SetActive(false);
        GameBuilder.Instance.RetryLevel(fromStart);
        soundControl.Click();
    }

    public void GetMoreHearts()
    {
        GameStats.SetHearts(5);
        RenewHearts();
        looseMenu.SetActive(false);
        GameBuilder.Instance.GameResume();
        soundControl.Click();

    }

    public void RenewHearts()
    {
        sliderHerat.value = GameStats.GetHearts();
    }

    public void CalibrateButton()
    {
        GameStats.gSensCorrective = -Input.gyro.gravity.x;
        DataControl.SaveData();
    }


}
