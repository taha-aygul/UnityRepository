using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emove : MonoBehaviour
{
    public GameObject gameOverScreen;
    //private GameObject[] Enemies;
   
   // IEnumerator sayac;
    private Rigidbody2D rb;
    private float direction;
    SpriteRenderer sprite;
    private Animator animator;

    void Start()
    {
       // StartCoroutine("Wait");
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        direction = -1;
        if(sprite.name == "pig" || sprite.name == "Bear")
        {
            direction = 1;
        }

    }
    void Update()
    {
        //Create();

        rb = GetComponent<Rigidbody2D>();
            Movement(rb);
    }
    void Movement(Rigidbody2D rb)
    {
        animator.SetBool("isMoving", true);
        if (direction == 1)
        {
            rb.velocity = new Vector2(3, rb.velocity.y);
        }
        else if (direction == -1)
        {
            rb.velocity = new Vector2(-3, rb.velocity.y);

        }

    }
    void Flip()

    {

        if (direction == -1)  // Karakter �u an sa�a bak�yor do�ru ama y�nelimi -1 yani sola do�ru 
        {
                if(sprite.flipX == true)
                {
                    sprite.flipX = false;

                }
                else if(sprite.flipX == false)
                {
                    sprite.flipX = true;

                }


            }
        else if (direction == +1)  // Karakter �u an sa�a bak�yor do�ru ama y�nelimi +1 yani sola do�ru 
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

    }

    public void changeDirection()
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAh");
        Wait();
        animator.SetBool("isMoving", false);
        Flip();
        this.direction = direction * -1;
    }
    IEnumerator Wait()                 // Fonksiyonu beklemmeli çağırmak için 
    {

        yield return new WaitForSeconds(1f);


    }

    private void Create() 
    {
       // Vector3 vv = new Vector3(-379,-130,0);
       // Quaternion qq = new Quaternion();
       // Instantiate(pig, vv,qq);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "wall")
        {
           changeDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        if (collisionInfo.collider.tag == "Player")
        {
            gameOverScreen.SetActive(true);
        }

    }



}
