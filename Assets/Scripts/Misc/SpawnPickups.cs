using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public GameObject[] collectiblePrefabArray;

    void Start()
    {
        int randomIndex = Random.Range(0, collectiblePrefabArray.Length);
        
        Instantiate (collectiblePrefabArray[randomIndex], transform.position, transform.rotation);
    }


}
