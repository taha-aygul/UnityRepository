using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopsController : CarController
{
    [SerializeField] private Transform target;
    private Vector3 steerVector, relativeVector;
    private float steerAngle;
   
    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }

    private void FixedUpdate()
    {

        relativeVector = target.transform.position - transform.position;
        Debug.DrawRay(transform.position, relativeVector, Color.green, 0.2f, false);
        base.Move();
        Steer();
    }


    protected override void GetDirection()
    {
        steerVector = transform.InverseTransformDirection(relativeVector);
        steerAngle = maxSteerAngle * (steerVector.x / steerVector.magnitude);
        print(steerAngle);


        moveInput = 1;


        // steerAngle = Vector3.Angle(steerVector, transform.forward);
        //steerInput = steerAngle / 180;
        // Debug.DrawRay(transform.position, steerVector, Color.blue);

        // move ýnput , steerInput
    }


    protected override void Steer()
    {

        foreach (var wheel in frontWheels)
        {
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, steerAngle, 0.6f);
        }

    }


}
