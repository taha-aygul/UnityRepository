using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave_PlayerMovement1 : MonoBehaviour
{
    
    private Rigidbody2D rigid;
    public Collider2D col2D;
    private float horizontal ,vertical , speed ;
    private Animator animator;
    SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        speed = 5;
    }

    void Update()
    {

        rigid = GetComponent<Rigidbody2D>();
        Debug.Log(rigid.position.x + " " + rigid.position.y);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        Move();
        Flip();

       /* if (Input.GetKeyDown(KeyCode.Space) && col2D.isTrigger)                 // Magarada jump  crouch yok
        {
            rigid.velocity = new Vector2(rigid.velocity.x, jump);
            animator.SetFloat("VerticalSpeed", 1);
        }
        else
        {
            animator.SetFloat("VerticalSpeed", 0);

        }
        
        
        if (Input.GetKeyDown(KeyCode.CapsLock) && isCrouched == false)
        {
            speed = 2;
            animator.SetBool("isCrouched", true);
            isCrouched = true;
        }
        else if (Input.GetKeyDown(KeyCode.CapsLock) && isCrouched == true)
        {
            speed = 5;
            rigid.velocity = new Vector2(rigid.velocity.x , rigid.velocity.y);
            animator.SetBool("isCrouched", false);
            isCrouched = false;
        }*/

    }

    void Move()   // Hareket Fonksiyonu horizontal deðiþkeni 1 veya -1 olarak gelir ve speed ile çarparýz yönümüz ve hýzýmýz belli olur
    {
        rigid.velocity = new Vector2(horizontal*speed , vertical * speed);
        if(horizontal != 0 || vertical != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }


    
      // Nasýl olacak anlamadým yaw
    void Flip()  // Karakterin yüzünün ne yöne bakacaðýný buldurur  , en baþta saða baktýðýný düþünerek kodladým
    {
        if( horizontal == -1)  // Karakter þu an saða bakýyor doðru ama yönelimi -1 yani sola doðru 
        {
            sprite.flipX = true;


        }else if ( horizontal == +1)  // Karakter þu an saða bakýyor doðru ama yönelimi -1 yani sola doðru 
        {
            sprite.flipX = false;
        }
        /*else
        {
            sprite.flipX = false;
        }*/


    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "stair")
        {
            Debug.LogError("ANANANAN");
            inStair = true;
            animator.SetBool("inStair", true);

        }
        else
        {
            inStair = false;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "stair")
        {
            animator.SetBool("inStair", false);

            inStair = false;

        }
    }*/



    /*  private void OnCollisionEnter2D(Collision2D collision)
      {
          Debug.Log("EGU");
          if(collision.collider.tag == "gem")
          {
              Debug.Log("Gem aldýn");
          }
          if (collision.collider.tag == "cherry")
          {
              Debug.Log("Gem aldýn");
          }
      }*/
















}
