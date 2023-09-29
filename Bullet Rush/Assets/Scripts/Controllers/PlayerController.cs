using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    [SerializeField] JoystickController joystick;
    [SerializeField] Rigidbody myRigid;
    [SerializeField] float speed, rotSpeed;
    [SerializeField] GameObject bullet, bulletSpawnPosition;
    private Vector3 playerDirection;
    private bool isShooting;
    private Transform target;
    public static PlayerController Instance;
    private Vector3 pastPos = new Vector3(0,0,10);

    [SerializeField] private float shootingSpeed;
    [SerializeField] private float shootingRange;

    private PlayerLevelManager _playerLevelManager;
    [SerializeField]private EnemyDetection _enemyDetection;



    private void Awake()
    {
        MakeSingleton();
    }

    private void Start()
    {
        _playerLevelManager = PlayerLevelManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDirection == Vector3.zero)
        {
            myRigid.angularVelocity = Vector3.zero;
        }
        Move();
        LookToDirection();
        DetectEnemy();
    }
    private void FixedUpdate()
    {
        if (target != null)
        {

            /*var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            
            // Smoothly rotate towards the target point.
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed );
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotSpeed);*/
        }
    }


    public void Move()
    {
        playerDirection = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);

        myRigid.velocity =  Vector3.Lerp(myRigid.velocity,     playerDirection * speed * Time.deltaTime , 0.5f);

    }

    public void LookToDirection()
    {
        
        var newPos = Vector3.Lerp(pastPos, transform.position + playerDirection, 0.5f);
        transform.LookAt( newPos);
        pastPos = transform.position + playerDirection;
    }

    public void DetectEnemy(Transform target)
    {
        /*this.target = target;
        transform.LookAt(Vector3.Lerp(pastPos, target.position, 0.5f));
        pastPos = target.position;
        if (!isShooting)
        {
          //  StartCoroutine(nameof(Shoot));
        }*/

    }
    private void DetectEnemy()
    {

       Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                target = hitCollider.transform;
                /*var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);

                // Smoothly rotate towards the target point.
                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed );
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotSpeed);*/
                pastPos = target.position;
                if (!isShooting)
                {
                    StartCoroutine(nameof(Shoot));
                }
            }
        }

    }

    private IEnumerator Shoot()
    {
        GameObject bulletGO = Instantiate(bullet, bulletSpawnPosition.transform);
        bulletGO.transform.SetParent(null);
        bulletGO.GetComponent<Bullet>().Target = target;
        isShooting = true;
        yield return new WaitForSeconds(shootingSpeed);
        isShooting = false;
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
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            if (shootingSpeed > 0.1f)
            {
           // shootingSpeed /= 2;

            }
            TimeAndScoreManager.Instance.Gain();
            Destroy(other.gameObject);
        }
      
    }




}
