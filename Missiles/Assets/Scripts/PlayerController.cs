using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;


    [SerializeField] private Rigidbody2D rb_2D;
    [SerializeField] private float speed, rotateSpeed;
    private float _input;
    private SpriteRenderer mySprite;



    private void Awake()
    {
        MakeSingleton();
    }

    // Update is called once per frame
    void Update()
    {
        _input = Input.GetAxis("Horizontal");
    }
    void FixedUpdate()
    {
        rb_2D.velocity = transform.up * speed * Time.deltaTime;
        rb_2D.angularVelocity = -1 * _input * rotateSpeed * Time.deltaTime;
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

    public void KillPlayer()
    {

        speed = 0;
        rotateSpeed = 0;

        // TODO Kýrmýzý ekran  UI iþlemleri vs vs
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Star"))
        {
            TimeAndScoreManager.Instance.IncreaseStarCount();
            UIManager.Instance.UpdateStarCount();
        }
    }
}
