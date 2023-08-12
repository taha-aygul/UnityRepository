using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterController : MonoBehaviour
{
   
    
    [Range(0,400)]public float speed;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward*speed* Time.deltaTime);
    }
}
