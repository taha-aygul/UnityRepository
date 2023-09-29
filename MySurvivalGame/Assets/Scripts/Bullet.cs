using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float bulletMoveSpeed;
    public float damageToZombie;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 6);
    }
    public void Fired(GameObject bulletStartPos)
    {
        rb.velocity = bulletStartPos.transform.forward * bulletMoveSpeed;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie") && !other.GetComponentInParent<Zombie>().IsDied)
        {
           other.GetComponentInParent<Zombie>().DecreaseHealth(damageToZombie);
        }
            Destroy(gameObject);
    }
}
