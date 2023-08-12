using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Rigidbody2D player;
    public Vector2 vector2;
    void Update()
    {
        transform.position = player.position + vector2;

    }
}
