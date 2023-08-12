using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public float maxVelocity, minVelocity;
    public GameObject splashAnimation, deadAnimation, comboAnimation;
    public Vector3 force, circleForce;
    private float _previousSpeed;
    public int comboCount;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        /* if(rb.velocity.y == 0)
         {
             print("Velocity is zero");
         }*/


        if (rb.velocity.y <= 0 && _previousSpeed >= 0)
        {
            rb.AddForce(force, ForceMode.VelocityChange);
        }

        if (rb.velocity.y >= 0 && _previousSpeed <= 0)
        {
            rb.AddForce(-force * 6, ForceMode.VelocityChange);
        }

        Vector3 vertical = rb.velocity;
        vertical.y = Mathf.Clamp(vertical.y, minVelocity, maxVelocity);
        rb.velocity = vertical;
        _previousSpeed = rb.velocity.y;
        //  rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Red"))
        {
            GameObject jump = Instantiate(deadAnimation, rb.position, Quaternion.identity);
            Destroy(jump, 1f);
            Invoke("CallOpenDeadUI", 0.3f);
            Invoke("CallRestart", 1.2f);
        }
        else if (collision.collider.CompareTag("Safe"))
        {
            if (comboCount >= 3)         // Çarptýðýný parçalama kýsmý
            {

            }
            comboCount = 0;
            Quaternion splashRotation = Quaternion.Euler(90, 0, 90);
            GameObject splash = Instantiate(splashAnimation,collision.collider.transform);// rb.position, splashRotation
            splash.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            UIManager.Instance.openFinish();
        }
        else if (other.CompareTag("Circle"))
        {
            CircleManager.Instance.ThrowPieces(other.gameObject);
            comboCount++;
            if (comboCount >= 2)
            {
                GameObject jump = Instantiate(comboAnimation, rb.position, Quaternion.identity);
                Destroy(jump, 1f);
            }
        }
    }


    private void CallRestart()
    {
        UIManager.Instance.Restart();
    }

    private void CallOpenDeadUI()
    {
        UIManager.Instance.openDeadUI();
    }


}
