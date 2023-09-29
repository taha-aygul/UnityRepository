using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    private PlayerController _player;
    [SerializeField] private Rigidbody2D rb_2D;
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private Collider2D myCollider;
    [SerializeField] private GameObject boom;
    [SerializeField]
    private float
        speed, rotateSpeed, speedIncreaseAmount = 1f, speedIncreaseInterval = 0.5f,
        lifeTime = 10;

    private float rotateAmount;
    private Vector2 direction;

    private IEnumerator Start()
    {
        StartCoroutine(nameof(IncreaseSpeed));
        _player = PlayerController.Instance;
        mySprite.enabled = true;
        myCollider.enabled = true;
        yield return new WaitForSeconds(lifeTime);
        MyDestroy();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsGameOver())
        {
            Invoke(nameof(MyDestroy), 0.5f);
        }
        GetDirection();
    }
    void FixedUpdate()
    {
        rb_2D.velocity = transform.up * speed * Time.deltaTime;
        rb_2D.angularVelocity = rotateAmount * rotateSpeed * Time.deltaTime;
    }

    private void GetDirection()
    {
        direction = (transform.position - _player.transform.position).normalized;       // normalize önemli -1 1 arasý bir þey istiyoruz
        rotateAmount = Vector3.Cross(direction, transform.up).z;
    }

    private void Boom()
    {
        GameObject explosion = Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }

    private void MyDestroy()
    {
        mySprite.enabled = false;
        myCollider.enabled = false;
        speed = 0;
        rotateSpeed = 0;
        Boom();
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Missile"))
        {
            MyDestroy();
        }
        else if (collider.CompareTag("Player"))
        {
            MyDestroy();
            PlayerController.Instance.KillPlayer();
            MissileSpawnManager.Instance.StopSpawning();
            GameManager.Instance.GameOver();
            
        }
    }


    public IEnumerator IncreaseSpeed()
    {

        yield return new WaitForSeconds(speedIncreaseInterval);
        speed += speedIncreaseAmount ;
        StartCoroutine(nameof(IncreaseSpeed));
    }
}
