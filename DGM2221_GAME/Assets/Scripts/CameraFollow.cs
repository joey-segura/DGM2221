using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLocation;
    public float followSpeed = 0.1f;
 
    void LateUpdate()
    {
        if (transform.position != playerLocation.position)
        {
            Vector3 playerPosition = new Vector3(playerLocation.position.x, playerLocation.position.y + 8, playerLocation.position.z - 6f);
            transform.position = Vector3.Lerp(transform.position, playerPosition, followSpeed);
        }
    }
}

