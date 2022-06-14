using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERJUMP,
        POWERSPEED,
        LIFE
    }

    public CollectibleType curCollectible;
    public AudioClip pickupSound;
    public AudioMixerGroup soundFXGroup;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(pickupSound, soundFXGroup);

            switch (curCollectible)
            {

                case CollectibleType.LIFE:
                    GameManager.instance.lives++;
                    break;

                case CollectibleType.POWERJUMP:
                    GameManager.instance.playerInstance.StartJumpForceChange();
                    break;

                case CollectibleType.POWERSPEED:
                    GameManager.instance.playerInstance.StartSpeedChange();
                    break;
            }
            Destroy(gameObject);
        }
    }
        
}
