using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;   //Player

    void LateUpdate()
    {
        //Check if player exists
        if (target == null)
            return;

        //Take 2D player position
        Vector2 targetPos2D = target.position;

        //Keep camera at a offset on the z axis
        transform.position = new Vector3(targetPos2D.x, targetPos2D.y, transform.position.z);
    }
}