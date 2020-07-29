
using Lean.Gui;
using QuantumTek.QuantumUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_StartScene : MonoBehaviour
{
    [SerializeField] private GameObject canvasPlayButton;
    [SerializeField] private GameObject canvasLevelsButton;
    [SerializeField] private GameObject canvasOptionButton;
    [SerializeField] private QUI_SceneTransition qUI_SceneTransition;
    [SerializeField] private QUI_OptionList cameras;
    [SerializeField] private QUI_OptionList choseLevelUi;
    [SerializeField] private List<GameObject> choseLevelCanvasList;

    [SerializeField] private LeanToggle musicToggle;
    [SerializeField] private LeanToggle smartAcceleration;
    [SerializeField] private Slider gSenseSlider;
    [SerializeField] private Button freeRunButton;
    private SoundControl soundControl;

    public QUI_Bar loadingBar;
    public GameObject loadingUI;

    public List<Button> lvlButtons;

    public Animator animator;

    private void Awake()
    {
        soundControl = SoundControl.Instance;
    }
    private void Start()
    {
        Time.timeScale = 1f;
        GameStats.playClick = false;
        musicToggle.Set(GameStats.useMusic);
        smartAcceleration.Set(GameStats.useSmartAcceleration);
        gSenseSlider.value = GameStats.gSensCoef;
        freeRunButton.interactable = GameStats.acceptFreeRun;

        GameStats.playClick = true;

        if (GameStats.useMusic)
        {
            MusicOn();
        }

        freeRunButton.interactable = GameStats.acceptFreeRun;
        for (int i = 1; i < lvlButtons.Count; i++)
        {
            if (i<= GameStats.openLevels)
            {
                lvlButtons[i].interactable = true;
            }
            else
            {
                lvlButtons[i].interactable = false;
            }
        }
    }

    public void OnPlayButtonClick()
    {
        canvasPlayButton.SetActive(true);
        soundControl.Click();
    }

    public void OnBackPlayButtonClick()
    {
        canvasPlayButton.SetActive(false);
        soundControl.Click();
    }
    public void OnLevelButtonClick()
    {
        canvasLevelsButton.SetActive(true);
        soundControl.Click();
    }

    public void OnBackLevelButtonClick()
    {
        canvasLevelsButton.SetActive(false);
        soundControl.Click();
    }

    public void StartLevel(int lvlNumber)
    {
        soundControl.Click();
        GameStats.StartLevel = lvlNumber;
        StartCoroutine(LoadSceneAsync("MainGameScene"));
    }

    public void ShowOption()
    {
        cameras.SetOption(GameStats.GetCurrentCamera());
        canvasOptionButton.SetActive(true);
        soundControl.Click();

    }
    public void OffOption()
    {
        canvasOptionButton.SetActive(false);
        soundControl.Click();
        DataControl.SaveData();
    }

    public void ChoseCamera()
    {
        GameStats.SetCurrentCamera(cameras.optionIndex);
        soundControl.Click();
    }

    public void ChoseLevel()
    {
        for (int i = 0; i < choseLevelCanvasList.Count; i++)
        {
            if (i == choseLevelUi.optionIndex)
            {
                choseLevelCanvasList[i].SetActive(true);
            }
            else
            {
                choseLevelCanvasList[i].SetActive(false);
            }
            
        }
        soundControl.Click();
    }

    public void FreeRunClick()
    {
        soundControl.Click();
        GameStats.StartLevel = 1;
        StartCoroutine(LoadSceneAsync("FreeRunScene"));
    }

    public void MusicOn()
    {
        GameStats.useMusic = true;
        soundControl.Click();
        soundControl.Music();
    }

    public void MusicOff()
    {
        GameStats.useMusic = false;
        soundControl.StopMusic();
        soundControl.Click();
    }


    public void SmartRotateOn()
    {
        GameStats.useSmartAcceleration = true;
        soundControl.Click();
    }

    public void SmartRotateOff()
    {
        GameStats.useSmartAcceleration = false;
        soundControl.Click();
    }

    public void SetGSense()
    {
        GameStats.gSensCoef = gSenseSlider.value;
        soundControl.Click();
    }



    protected IEnumerator LoadSceneAsync(string sceneName)
    {
        loadingUI.SetActive(true);
        yield return new WaitForEndOfFrame();
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        while (!loadOperation.isDone)
        {
            float loadProgress = Mathf.Clamp01(loadOperation.progress / 0.9f);

            if (loadingBar)
                loadingBar.SetFill(loadProgress);

            yield return new WaitForEndOfFrame();
        }
    }

    public void OnBoyTap()
    {
        animator.SetTrigger("wave");
        //GameStats.acceptFreeRun = !GameStats.acceptFreeRun;
        //DataControl.SaveData();
    }

    public void CalibrateButton()
    {
        GameStats.gSensCorrective = -Input.gyro.gravity.x;
        DataControl.SaveData();
    }
}
