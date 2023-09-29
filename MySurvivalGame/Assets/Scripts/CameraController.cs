using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cameraRoot;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraRoot.transform.position;
        transform.eulerAngles = cameraRoot.transform.eulerAngles;
    }
}
