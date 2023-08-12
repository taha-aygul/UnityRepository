
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public float Force_X ;
    public float Force_Y;
    public Vector3 finishPoint;
    float  a = -1f;
    
    void Update()
    {


        /* if (rb.position.x <= 245f || rb.position.x > 350)
         {
             rb.transform.position += new Vector3(rb.position.x, 0, rb.position.z) * Time.deltaTime;
            // rb.velocity.Set(0,0,0);
             //rb.AddForce(0, -100, 0);

         }*/

        //Debug.Log(rb.velocity.x);

        /*  if (rb.velocity.x == 67)
          {
              rb.transform.position += new Vector3(rb.position.x, 0, rb.position.z) * Time.deltaTime;


          }*/



        if (rb.position.y > 19f && rb.position.x > 370)
        {
            rb.AddForce(0, -20, 0, ForceMode.VelocityChange);

            rb.velocity.Set(rb.velocity.x,0,0);

        }

        if (rb.velocity.x < 67)
        {

            rb.AddForce(Force_X, 0, 0, ForceMode.VelocityChange);

        }
       
        //VelocityBorder();

        //rb.AddForce(Force_X, 0, 0, ForceMode.VelocityChange);


        if (Input.GetKey(KeyCode.Space))
        {

            rb.AddForce(0, (float)0.1, 0, ForceMode.VelocityChange);

        }
        /* if (Input.GetKey("w"))
         {

             rb.AddForce(Force_X, 0, 0, ForceMode.VelocityChange);

         }
         if (Input.GetKey("s"))
         {

             rb.AddForce(-Force_X, 0, 0, ForceMode.VelocityChange);

         }*/



        if (Input.GetKey("d"))
        {

            rb.AddForce(0, 0, -Force_Y ,ForceMode.VelocityChange);

        }else if (Input.GetKey("a"))
        {
            rb.AddForce(0, 0, Force_Y , ForceMode.VelocityChange);

        }

        if (rb.position.y  < 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
    public void Stop()
    {
        
        finishPoint = rb.position;
        rb.velocity.Set(0,0,0);

        rb.MovePosition(finishPoint);
    }



}
