using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Degýstiiiiiiiiiiiiiii
    private Rigidbody2D rigid;
    public Collider2D isGrounded;
    public Collider2D Ground;

    private float horizontal ,vertical , speed , jump = 7;
    private bool isCrouched = false , inStair = false ;
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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");


        if (inStair)
        {
            MoveVertical();
        }
        Move();
        Flip();
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded.IsTouching(Ground))
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
        }

    }

    void Move()   // Hareket Fonksiyonu horizontal deðiþkeni 1 veya -1 olarak gelir ve speed ile çarparýz yönümüz ve hýzýmýz belli olur
    {
        rigid.velocity = new Vector2(horizontal*speed , rigid.velocity.y);
        if(horizontal != 0)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }


    public void MoveVertical()
    {
        rigid.velocity = new Vector2(horizontal * speed, vertical * speed);
        
    }







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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "stair")
        {
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
    }



  









}
