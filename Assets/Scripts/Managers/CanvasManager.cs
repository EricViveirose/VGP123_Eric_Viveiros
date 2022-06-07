using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startButton;
    public Button settingsButton;
    public Button backButton;
    public Button quitButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public Text livesText;
    public Text volSliderText;

    [Header("Slider")]
    public Slider volSlider;

    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void ShowMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void OnSliderValueChanged(float value)
    {
        if (volSliderText)
            volSliderText.text = value.ToString();
    }

    void OnLifeValueChange(int value)
    {
        if (livesText)
            livesText.text = value.ToString();
    }

    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(() => StartGame());

        if (settingsButton)
            settingsButton.onClick.AddListener(() => ShowSettingsMenu());

        if (backButton)
            backButton.onClick.AddListener(() => ShowMainMenu());

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => ShowMainMenu());

        //if (returnToGameButton)
        //    returnToGameButton.onClick.AddListener(() => Time.timeScale = 0());

        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
            if (volSliderText)
                volSliderText.text = volSlider.value.ToString();
        }

        //Add Listener to Lives value change
        if (livesText)
            GameManager.instance.OnLifeValueChanged.AddListener((value) => OnLifeValueChange(value));
}


void Update()
        {
            if (pauseMenu)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    pauseMenu.SetActive(!pauseMenu.activeSelf);

                    //HINT FOR THE LAB
                    if (pauseMenu.activeSelf)
                    {
                    Time.timeScale = 0;
                    }
                    else
                    {
                    Time.timeScale = 1;
                    }
                }
            }

        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
        }
    }