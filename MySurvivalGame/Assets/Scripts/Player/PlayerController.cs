using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float
        velocity = 5,
        sprintVelocity = 10,
        currentVelocity = 0,
        jumpVelocity = 10,
        jumpCooldown = 0.1f,
        airMultiplier = 1,
        playerHeight = 1;


    public KeyCode jumpKey = KeyCode.Space;
    public LayerMask Ground;

    bool grounded,
        readyToJump,
        isControlling,
        playerIsAlive = true;

    private float horizontal, vertical;
    public GameObject cameraRoot;
    public CharacterController characterController;
    protected float yaw;
    protected float pitch;
    public float MinYaw = -360;
    public float MaxYaw = 360;
    public float MinPitch = -60;
    public float MaxPitch = 60;
    public float LookSensitivity = 1;
    private Vector3 moveDirection;
    public Gun gunSc;

    [SerializeField]
    private float
        maxHealth = 100,
        health;

    [SerializeField]
    private float
        walkBobSpeed = 14f,
        walkBobAmount = 0.5f,
        sprintBobSpeed = 18f,
        sprintBobAmount = 1f;

    private float
        defaultYPos = 0,
        timer;

    public static PlayerController Instance;
    private bool isSprinting;

    public float Health { get => health; set => health = value; }
    public bool PlayerIsAlive { get => playerIsAlive; set => playerIsAlive = value; }

    private void Awake()
    {
        MakeSingleton();
        characterController = GetComponent<CharacterController>();
    }



    private void Start()
    {
        health = maxHealth;
        defaultYPos = cameraRoot.transform.localPosition.y;
        isControlling = true;

    }


    private void Update()
    {
        if (playerIsAlive && GameManager.Instance.GameStarted && !GameManager.Instance.IsGameOver())
        {
            HandleInput();
            HandleHeadBob();
        }
    }


    private void FixedUpdate()
    {

        if (playerIsAlive && GameManager.Instance.GameStarted && !GameManager.Instance.IsGameOver())
        {
            Move();
        }
    }

    private void LateUpdate()
    {

        if (playerIsAlive && GameManager.Instance.GameStarted && !GameManager.Instance.IsGameOver())
        {
            CameraLook();
        }
    }

    private void Move()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);
        moveDirection = cameraRoot.transform.forward * vertical + cameraRoot.transform.right * horizontal;
        moveDirection.Normalize();


        if (Input.GetKey(KeyCode.LeftShift))  // Player can sprint by holding "Left Shit" keyboard button
        {
            currentVelocity = sprintVelocity;
            isSprinting = true;
        }
        else
        {
            currentVelocity = velocity;
            isSprinting = false;
        }


        if (grounded)
        {
            moveDirection.y = 0;
        }
        else
        {
            moveDirection.y = -airMultiplier * 9.81f * Time.deltaTime;
        }

        //  print(moveDirection + " " + grounded);

        characterController.Move(moveDirection * Time.deltaTime * currentVelocity);


        /*  if (grounded)
              myRigid.AddForce(moveDirection.normalized * velocity * 10f, ForceMode.Force);

          // in air
          else if (!grounded)
              myRigid.AddForce(moveDirection.normalized * velocity* 10f * airMultiplier, ForceMode.Force);
        */
    }


    private void HandleHeadBob()
    {
        if (Math.Abs(moveDirection.x) > 0.1f || Math.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isSprinting ? sprintBobSpeed : walkBobSpeed);
            cameraRoot.transform.localPosition = new Vector3(
               cameraRoot.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isSprinting ? sprintBobAmount : walkBobAmount),
                cameraRoot.transform.localPosition.z);
        }
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");




        if (Input.GetKeyDown(jumpKey))
        {
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Fire();
        }



    }

    private void Fire()
    {
        gunSc.FireBullet();
    }

    private void CameraLook()
    {
        yaw += Input.GetAxisRaw("Mouse X") * LookSensitivity;
        pitch -= Input.GetAxisRaw("Mouse Y") * LookSensitivity;

        yaw = ClampAngle(yaw, MinYaw, MaxYaw);
        pitch = ClampAngle(pitch, MinPitch, MaxPitch);

        transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);
        cameraRoot.transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
    }

    protected float ClampAngle(float angle)
    {
        return ClampAngle(angle, 0, 360);
    }

    protected float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    public void ToggleControl()
    {
        Cursor.lockState = (isControlling) ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isControlling;
    }


    public void UpdateHealth(int amount)
    {
        health -= amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            playerIsAlive = false;
            GameManager.Instance.GameOver();
        }

        UIManager.Instance.UpdateHealth();
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

/*  private void Move()
  {
      Vector3 direction = Vector3.zero;
      direction += transform.forward * Input.GetAxisRaw("Vertical");
      direction += transform.right * Input.GetAxisRaw("Horizontal");

      direction.Normalize();


      if (Input.GetKey(KeyCode.LeftShift))  // Player can sprint by holding "Left Shit" keyboard button
      {
          currMoveSpeed = SprintSpeed;
      }
      else
      {
          currMoveSpeed = MoveSpeed;
      }

      direction += velocity * Time.deltaTime;
      movementController.Move(direction * Time.deltaTime * currMoveSpeed);
  }*/