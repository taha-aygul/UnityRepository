using System.Collections;
using UnityEngine;

public class Cave_Enemy : MonoBehaviour
{
    public GameObject Slime;
    public GameObject gameOverScreen;
    //private GameObject[] Enemies;
    // IEnumerator sayac;
    private Rigidbody2D rb;
    private float directionH;
    private float directionV;
    private float VelocityX =3;
    private float VelocityY=3;

    SpriteRenderer sprite;
    private Animator animator;
    private int i = 0;
    void Start()
    {
        // Create();

        // StartCoroutine("Wait");

        directionV = 0;
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (i%2 == 0)
        {
            directionV = 1;
        }
        directionH = -1;
        if (sprite.name == "pig" || sprite.name == "Bear")
        {
            directionH = 1;
        }

    }
    void Update()
    {
        

        rb = GetComponent<Rigidbody2D>();
        Movement(rb);
    }
    void Movement(Rigidbody2D rb)
    {
        animator.SetBool("isMoving", true);
        if (directionH == 1)
        {
            if(directionV == 1)
            {
                rb.velocity = new Vector2(VelocityX, VelocityY);

            }
            else
            {
                rb.velocity = new Vector2(VelocityX, -VelocityY);

            }



        }
        else if (directionH == -1)
        {
            if (directionV == 1)
            {
                rb.velocity = new Vector2(-VelocityX, VelocityY);

            }
            else
            {
                rb.velocity = new Vector2(-VelocityX, -VelocityY);

            }
        }

    }
    void Flip()

    {
        
            if (sprite.flipX == true)
            {
                sprite.flipX = false;

            }
            else if (sprite.flipX == false)
            {
                sprite.flipX = true;

            }

    }

    public void changeDirection()
    {
        Wait();
        animator.SetBool("isMoving", false);
        Flip();
        this.directionH = directionH * -1;
        this.directionV = directionV *-1;

    }
    IEnumerator Wait()                 // Fonksiyonu beklemmeli çağırmak için 
    {

        yield return new WaitForSeconds(1f);


    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "wall" )
        {
            changeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        if (collisionInfo.collider.tag == "Player")
        {
            gameOverScreen.SetActive(true);
        }else if (collisionInfo.collider.tag == "wall" || collisionInfo.collider.tag == "border")
        {
            changeDirection();
        }

    }




}
