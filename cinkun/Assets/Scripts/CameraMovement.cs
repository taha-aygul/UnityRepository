using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    public Transform player;
    public Vector3 vector3;
    void Update()
    {
        transform.position = player.position + vector3;

    }
}
