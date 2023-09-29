using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position -target.position ;
    }

    private void LateUpdate()
    {

        transform.position = new Vector3(target.position.x + offset.x, target.position.y + offset.y, 0);

    }
}
