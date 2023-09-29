using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
   private Vector3 offset, position;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        position = offset + target.position;
        transform.position = Vector3.Lerp(transform.position, position, .5f);
    }
}
