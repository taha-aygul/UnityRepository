
using UnityEngine;

public class Follower_Camera : MonoBehaviour
{


    public Transform player;
    public Vector3 vector3;
    void Update()
    {
        transform.position = player.position + vector3;

    }
}
