using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 direction;
    public float multiplier;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.eulerAngles;
        direction.x = Mathf.Cos(direction.y);
        direction.z= Mathf.Sin(direction.y);
        direction.y = 0;
    }

    private void Update()
    {
        rb.AddForce(direction * multiplier);
            
    }
}
