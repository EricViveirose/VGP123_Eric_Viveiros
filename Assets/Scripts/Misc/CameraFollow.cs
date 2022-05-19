using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    public float minXClamp = -0.26f;
    public float maxXClamp = 41.84f;
    public float minYClamp = -9.44f;
    public float maxYClamp = 0.23f;

     void LateUpdate()
    {
        if (player)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);

            transform.position = cameraTransform;
        }

        if (player)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.y = player.transform.position.y;
            cameraTransform.y = Mathf.Clamp(cameraTransform.y, minYClamp, maxYClamp);

            transform.position = cameraTransform;
        }
    }

}
