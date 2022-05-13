using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{

    public enum CollectibleType
    {
        POWERJUMP,
        POWERSPEED,
        LIFE
    }

    public CollectibleType curCollectible;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController curPlayer = collision.gameObject.GetComponent<PlayerController>();

            switch (curCollectible)
            {

                case CollectibleType.LIFE:
                    curPlayer.lives++;
                    break;
                case CollectibleType.POWERJUMP:
                    curPlayer.StartJumpForceChange();
                    break;
                case CollectibleType.POWERSPEED:
                    curPlayer.StartSpeedChange();
                    break;
            }
            Destroy(gameObject);
        }
    }
        
}
