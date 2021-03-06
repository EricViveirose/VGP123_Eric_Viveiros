using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    [Header("Images")]
    public Image[] hearts;

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

    public AudioMixer mixer;
    public AudioClip pauseSFX;
    public AudioMixerGroup soundFXMixer;

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

        if (mixer)
            mixer.SetFloat("MasterVol", value);
    }

    void OnLifeValueChange(int value)
    {
        if (livesText)
            livesText.text = value.ToString();

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.instance.lives)
                hearts[i].gameObject.SetActive(true);
            else
                hearts[i].gameObject.SetActive(false);
        }
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
            returnToMenuButton.onClick.AddListener(() => ReturnToMenu());

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(() => ReturnToGame());

        if (quitButton)
            quitButton.onClick.AddListener(() => QuitGame());

        if (volSlider)
        {
            volSlider.onValueChanged.AddListener((value) => OnSliderValueChanged(value));
            if (volSliderText)
                volSliderText.text = volSlider.value.ToString();
        }

        //Add Listener to Lives value change
            GameManager.instance.OnLifeValueChanged.AddListener((value) => OnLifeValueChange(value));
}


void Update()
        {
            if (pauseMenu)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    pauseMenu.SetActive(!pauseMenu.activeSelf);


                if (pauseMenu.activeSelf)
                    {
                        Time.timeScale = 0;
                        GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(pauseSFX, soundFXMixer);
                        GameManager.instance.playerInstance.GetComponent<PlayerController>().enabled = false;
                        GameManager.instance.playerInstance.GetComponent<ShootProjectile>().enabled = false;
                    }
                    else
                    {
                        Time.timeScale = 1;
                        GameManager.instance.playerInstance.GetComponent<PlayerController>().enabled = true;
                        GameManager.instance.playerInstance.GetComponent<ShootProjectile>().enabled = true;
                    }
                }
            }
        }

    public void ReturnToGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameManager.instance.playerInstance.GetComponent<PlayerController>().enabled = true;
        GameManager.instance.playerInstance.GetComponent<ShootProjectile>().enabled = true;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Start");
        Time.timeScale = 1;
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