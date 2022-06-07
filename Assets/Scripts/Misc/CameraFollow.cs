using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float minXClamp = -0.26f;
    public float maxXClamp = 41.84f;
    public float minYClamp = -9.44f;
    public float maxYClamp = 0.23f;

     void LateUpdate()
    {
        if (GameManager.instance.playerInstance)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = GameManager.instance.playerInstance.gameObject.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);

            cameraTransform.y = GameManager.instance.playerInstance.gameObject.transform.position.y;
            cameraTransform.y = Mathf.Clamp(cameraTransform.y, minYClamp, maxYClamp);

            transform.position = cameraTransform;
        }

    }

}
