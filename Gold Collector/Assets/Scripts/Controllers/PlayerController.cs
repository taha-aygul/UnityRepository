using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed, runningSpeed, walkingSpeed, bonusSpeedFactor, bonusSpeedTime;
    public float  jumpPower, bonusJumpFactor, bonusJumpTime, score;
    private bool _isLanded, _isBonusJumped, _isBonusSpeed;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();

    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        x *= speed * Time.deltaTime;
        y *= speed * Time.deltaTime;

        transform.Translate(new Vector3(x, 0, y));

    }


    private void Jump()
    {
        if (_isLanded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector3(0, jumpPower, 0));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeadZone"))
        {
            UIManager.Instance.GameOver();
        }
        else if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            score += 1;
        }
        else if (other.CompareTag("Gem"))
        {
            Destroy(other.gameObject);
            score += 10;
        }
        else if (other.CompareTag("BonusJump") && !_isBonusJumped)
        {
            StartCoroutine(BonusJump());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BonusSpeed") && !_isBonusSpeed)
        {
            StartCoroutine(BonusSpeed());
            Destroy(other.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("DeadZone"))
        {
            UIManager.Instance.GameOver();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        _isLanded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        _isLanded = false;
    }


    private IEnumerator BonusJump()
    {
        _isBonusJumped = true;
        jumpPower *= bonusJumpFactor;
        yield return new WaitForSeconds(bonusJumpTime);
        jumpPower /= bonusJumpFactor;
        _isBonusJumped = false;
    }

    private IEnumerator BonusSpeed()
    {
        _isBonusSpeed = true;
        runningSpeed *= bonusSpeedFactor;
        yield return new WaitForSeconds(bonusSpeedTime);
        runningSpeed /= bonusSpeedFactor;
        _isBonusSpeed = false;

    }


   
}
