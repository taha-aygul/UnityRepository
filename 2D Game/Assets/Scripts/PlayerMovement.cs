using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
	private float horizontal;
	private float speed = 8f;
	public float jumping_power = 16f;
	private bool isFacingRight;

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;

    private void Update()
    {
		horizontal = Input.GetAxisRaw("Horizontal");
		Flip();

        if (Input.GetKeyDown(KeyCode.Space)){

			rb.velocity = new Vector2(rb.velocity.x, jumping_power);
        }
		if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
		{

			rb.velocity = new Vector2(rb.velocity.x, jumping_power*0.5f);
		}

	}

	private void FixedUpdate()
    {
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

	private void Flip()
    {
		if(isFacingRight && horizontal < 0f   ||  !isFacingRight && horizontal > 0f)
        {
			isFacingRight = !isFacingRight;
			Vector3 localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
        }
    }


}