using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KazıkController : MonoBehaviour
{

    public Vector3 distance; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.position + distance);
    }




}
