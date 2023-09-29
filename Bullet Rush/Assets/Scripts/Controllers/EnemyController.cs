using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] ParticleSystem.TriggerModule dying;
    [SerializeField] GameObject player;
    [SerializeField] private int health ;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;
    private Vector3 direction;
    private PlayerController _playerController;
    [SerializeField] GameObject coin;
    [SerializeField] private int coinProbability;
    [SerializeField] GameObject dyingAnim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerController = PlayerController.Instance;   
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }

    private void Move()
    {
        direction = (_playerController.transform.position - transform.position ).normalized;
        rb.velocity = direction * speed * Time.deltaTime;
    }

    private void Look()
    {
        transform.LookAt(_playerController.transform.position);
    }

    

     // TODO OnDestroy a al burayý
     public void Die()
    {

       // dying.Play();
        GameObject dyingGO = Instantiate(dyingAnim, transform.position, Quaternion.identity);
        dying = dyingGO.GetComponent<ParticleSystem>().trigger;
        dying.AddCollider( PlayerController.Instance.GetComponent<Collider>());
        int r = UnityEngine.Random.Range(0, 100);
       /* gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;*/

        if (r < coinProbability)
        {
            Instantiate(coin, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

    }
}
