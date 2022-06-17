using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    static GameManager _instance = null;
    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    private int _lives = 1;
    public int maxLives = 3;    


    public int lives
    {
        get { return _lives; }
        set
        {
            if (_lives > value)
            {
                //Destroy(playerInstance.gameObject);
                //SpawnPlayer(currentLevel.spawnPoint);
                playerInstance.transform.position = currentLevel.spawnPoint.position;
                playerInstance.sfxManager.Play(playerInstance.playerDeathSound, playerInstance.soundFXGroup);
            }

            _lives = value;
            OnLifeValueChanged.Invoke(value);

            if (_lives > maxLives)
                _lives = maxLives;

            if (_lives <= 0)
                GameOver();
         
            Debug.Log("Lives Set To: " + lives.ToString());
        }
    }

    public PlayerController playerPrefab;
    [HideInInspector] public PlayerController playerInstance;
    [HideInInspector] public Level currentLevel;
    [HideInInspector] public UnityEvent<int> OnLifeValueChanged;

    void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    if (SceneManager.GetActiveScene().name == "Start")
        //        SceneManager.LoadScene("Level");

        //    else if (SceneManager.GetActiveScene().name == "GameOver")
        //        SceneManager.LoadScene("Start");

        //    //else if (SceneManager.GetActiveScene().name == "Level")
        //    //    SceneManager.LoadScene("GameOver");
        //}
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        Debug.Log("GO TO GAME OVER SCREEN");
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        playerInstance = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation);
    }
}
