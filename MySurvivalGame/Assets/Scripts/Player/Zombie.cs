using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public TerrainManager terrainManager;
    public Animator animator;
    public GameObject heartPrefab;
    private PlayerController _playerController;
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool isDied, iSawPlayer;



    [SerializeField]
    private float
        startHealth = 100, currentHealth, runSpeed, walkSpeed;
    [SerializeField]
    private int damage = 0;


    [SerializeField]
    private float
        detectRadius, viewDistance = 40f, closeDistance = 10f, farDistance = 100f, viewAngle = 135f;


    private float patrolChance;
    [Range(0, 100)] [SerializeField] private float patrolProbability;

    [SerializeField]
    private float
        maxPatrolDistance, minPatrolDistance, minimumDistanceToBorder;
    bool isPatrolling;

    public bool IsDied { get => isDied; set => isDied = value; }

    private void Awake()
    {
        terrainManager = TerrainManager.Instance;
        currentHealth = startHealth;
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        _playerController = PlayerController.Instance;

    }
    void Start()
    {


        isPatrolling = WillItPatrol();
        if (isPatrolling)
        {
            StartCoroutine(nameof(Patrol));
        }
        StartCoroutine(nameof(MeasureDistanceToPlayer));
    }

    // Update is called once per frame
    void Update()
    {

        isPlayerTooCloseOrTooAway();
        MoveAtPlayer();

    }


    private void MoveAtPlayer()
    {
        if (!isDied && iSawPlayer && !animator.GetBool("isAttacking") && !animator.GetBool("isIdle"))
        {
            Vector3 direction = _playerController.transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 10 * Time.deltaTime);

            agent.SetDestination(_playerController.transform.position);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }

    private void isPlayerTooCloseOrTooAway()
    {
        if (Vector3.Distance(transform.position, _playerController.transform.position) < closeDistance && !isDied && !animator.GetBool("isAttacking"))
        {
            ActivateRun();
        }

        if (Vector3.Distance(transform.position, _playerController.transform.position) > farDistance && !isDied)
        {
            ActivateWalk();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;
        if (other.CompareTag("Player"))
        {
            ActivateAttack();
        }
    }

    private IEnumerator MeasureDistanceToPlayer()             // Animation event
    {

        if (Vector3.Distance(transform.position, _playerController.transform.position) < viewDistance && !animator.GetBool("isAttacking"))
        {
            Vector3 directionToPlayer = (_playerController.transform.position - transform.position).normalized;
            float angleBetweenZombieAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenZombieAndPlayer < viewAngle / 2)
            {
                if (Physics.Linecast(transform.position, _playerController.transform.position))
                {
                    if (animator.GetBool("isRunning") == false)
                    {
                        ActivateRun();
                    }
                    agent.SetDestination(_playerController.transform.position);
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(nameof(MeasureDistanceToPlayer));

    }


    public void CheckPlayerInRangeOnHit()               // Animation Event
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                _playerController.UpdateHealth(damage);
            }
        }

    }

    public void CheckPlayerInRangeAfterHit()               // Animation Event
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                agent.SetDestination(transform.position);

                return;
            }
        }
        rb.isKinematic = false;
        ActivateRun();
    }

    public void DecreaseHealth(float damage)
    {
        Collider[] colList = transform.GetComponentsInChildren<Collider>();


        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            for (int i = 0; i < colList.Length - 1; i++)
            {
                colList[i].enabled = false;
            }
            StopAllCoroutines();
            StopCoroutine(nameof(Patrol));
            isDied = true;
            animator.SetTrigger("isDied");
            agent.SetDestination(transform.position);
            gameObject.layer = LayerMask.NameToLayer("DiedZombie");
            rb.isKinematic = true;
            Destroy(gameObject, 10f);
            TimeAndScoreManager.Instance.AddScore(10);
        }
        else
        {
            if (!isDied)
            {
                ActivateRun();
            }
        }

    }



    private bool WillItPatrol()
    {
        patrolChance = UnityEngine.Random.Range(0, 100);
        if (patrolChance < patrolProbability)
        {
            ActivateWalk();
            return true;

        }
        return false;
    }
    private void SetPatrolDestination() // 50 50 -- 450 50 --- 450 350 - 50 350
    {
        int RandomPosX = Convert.ToInt32(UnityEngine.Random.Range(terrainManager.GetTerrainBounds_minX() + minimumDistanceToBorder, terrainManager.GetTerrainBounds_maxX() - minimumDistanceToBorder));
        int RandomPosZ = Convert.ToInt32(UnityEngine.Random.Range(terrainManager.GetTerrainBounds_minZ() + minimumDistanceToBorder, terrainManager.GetTerrainBounds_maxZ() - minimumDistanceToBorder));


        float height = terrainManager.GetHeight(RandomPosX, RandomPosZ);
        Vector3 patrollingDestination = new Vector3(RandomPosX, height, RandomPosZ);

        while (
               Vector3.Distance(transform.position, patrollingDestination) < minPatrolDistance
            && Vector3.Distance(transform.position, patrollingDestination) > maxPatrolDistance
            && Physics.Linecast(transform.position, patrollingDestination))
        {
            RandomPosX = Convert.ToInt32(UnityEngine.Random.Range(terrainManager.GetTerrainBounds_minX() + minimumDistanceToBorder, terrainManager.GetTerrainBounds_maxX() - minimumDistanceToBorder));
            RandomPosZ = Convert.ToInt32(UnityEngine.Random.Range(terrainManager.GetTerrainBounds_minZ() + minimumDistanceToBorder, terrainManager.GetTerrainBounds_maxZ() - minimumDistanceToBorder));
            height = terrainManager.GetHeight(RandomPosX, RandomPosZ);
            patrollingDestination = new Vector3(RandomPosX, height, RandomPosZ);
        }
        agent.SetDestination(patrollingDestination);
        //ActivateWalk();
    }

    private IEnumerator Patrol()
    {

        if (agent.isOnNavMesh)
        {
            if (!isDied && !iSawPlayer && isPatrolling)
            {
                SetPatrolDestination();
            }
            yield return new WaitUntil(() => agent.remainingDistance <= 2);// && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0);
            StartCoroutine(nameof(Patrol));
        }
        else
        {
            Destroy(gameObject);
        }



    }

    private void ActivateRun()
    {
        StopCoroutine(nameof(Patrol));
        iSawPlayer = true;
        animator.SetBool("isRunning", true);
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", false);
        agent.SetDestination(_playerController.transform.position);
        agent.speed = runSpeed;
    }

    private void ActivateAttack()
    {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        agent.SetDestination(transform.position);

    }

    private void ActivateWalk()
    {
        agent.speed = walkSpeed;
        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", true);
        iSawPlayer = false;
    }




}

