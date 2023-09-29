using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    private Vector3 direction;
    [SerializeField] float speed;
    public Transform Target { get; set; }


    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {


        if (Target != null)
        {
            transform.LookAt(Target);
            direction = (Target.position - transform.position).normalized;
            rb.velocity = direction * speed * Time.deltaTime;
        }
        else
        {
            direction = transform.up;
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            // Enemy Destroy
            other.GetComponent<EnemyController>().Die();
            // TODO playere skor ekle, enemy öldür
        }
            Destroy(gameObject);
    }
}
