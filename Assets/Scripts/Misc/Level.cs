using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int StartingLives = 3;
    public Transform spawnPoint;
 
    void Start()
    {
        GameManager.instance.lives = StartingLives;
        GameManager.instance.currentLevel = this;
        GameManager.instance.SpawnPlayer(spawnPoint);
    }

}