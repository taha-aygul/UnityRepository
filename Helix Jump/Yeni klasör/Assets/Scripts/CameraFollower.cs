using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public Transform target;
    private Vector3 offset, current;
    public Vector3 startPos;

  


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
        transform.position = offset + target.position;

    }

    private void LateUpdate()
    {
        current = offset + target.position;
        if (current.y > transform.position.y)
            return;

        transform.position = offset + target.position;
    }

    public void GoToStartPosition()
    {
        transform.position = startPos;
    }
   
}
