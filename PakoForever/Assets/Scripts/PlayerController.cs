using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController
{
    public static PlayerController Instance;

    private void Awake()
    {
        MakeSingleton();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }
    private void FixedUpdate()
    {
        base.Move();
        base.Steer();
    }

    protected override void GetDirection()
    {
        moveInput = 1; Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }


    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
