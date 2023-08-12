using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public int movePower;
    public int jumpPower;
	private float horizontal;
    private bool isFacingRight = true;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Debug.Log(horizontal + " " + isFacingRight);

        horizontal = Input.GetAxisRaw("Horizontal");
        Flip();

        if (Input.GetKey(KeyCode.D))
        {
            Run();
            isFacingRight = true;
            rb.velocity = new Vector2(movePower, 0);

        }
        else if (Input.GetKey(KeyCode.A))
        {
            Run();
            isFacingRight = false;
            rb.velocity = new Vector2(-movePower,0);

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpPower);

        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(0, jumpPower-0.5f);

        }
    }



    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Run()
    {
        if(horizontal != 0)
        {
            animator.SetFloat("velocity", 1);
        }
        else
        {
            animator.SetFloat("velocity", 1);

        }

    }
}
