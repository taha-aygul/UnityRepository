using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] protected float maxAcceleration = 30.0f;
    [SerializeField] protected float brakeAcceleration = 50.0f;

    [SerializeField] protected float turnSensitivity = 1.0f;
    [SerializeField] protected float maxSteerAngle = 30.0f;

    [SerializeField] protected Vector3 _centerOfMass;

    [SerializeField] protected List<WheelCollider> frontWheels;
    [SerializeField] protected List<WheelCollider> rearWheels;


    protected float moveInput;
    protected float steerInput;

    private Rigidbody carRb;


    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

    }

    void Update()
    {
        GetDirection();
    }

     void FixedUpdate()
    {
        Move();
        Steer();
    }

    protected virtual void GetDirection()
    {
        throw new NotImplementedException();
    }

   

    protected virtual void Steer()
    {
        foreach (var wheel in frontWheels)
        {
            var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
            wheel.steerAngle = Mathf.Lerp(wheel.steerAngle, _steerAngle, 0.6f);
        }
    }

    protected void Move()
    {
        foreach (var wheel in frontWheels)
        {
            wheel.motorTorque = moveInput * maxAcceleration * Time.deltaTime;
        }
        foreach (var wheel in rearWheels)
        {
            wheel.motorTorque = moveInput * maxAcceleration * Time.deltaTime;
        }
    }

}
