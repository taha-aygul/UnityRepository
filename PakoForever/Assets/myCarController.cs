using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCarController : MonoBehaviour
{

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Vector3 _centerOfMass;

    [SerializeField] List<WheelCollider> frontWheels;
    [SerializeField] List<WheelCollider> rearWheels;


    float moveInput;
    float steerInput;

    private Rigidbody carRb;


    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

    }

    void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        Move();
        Steer();
    }

    private void Steer()
    {

    }

    private void Move()
    {
        foreach (var wheel in frontWheels)
        {
            wheel.motorTorque = moveInput * maxAcceleration * Time.deltaTime;
        }
    }
}
