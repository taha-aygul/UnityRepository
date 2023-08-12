using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{

    public Vector3 direction;
    public float rotatingFactor;
    public bool manuelSelect;
   

    // Update is called once per frame
    void Update()
    {
        RotatePiece();
    }


    public void RotatePiece()
    {
        if (!manuelSelect)
        {
            direction = new Vector3(0, 90, 0);
            rotatingFactor = 1;
        }
    transform.Rotate(direction *Time.deltaTime * rotatingFactor );
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Safe") || collision.collider.CompareTag("Red"))
        {
            print(collision.collider.tag);

            ChangeDirection();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
       // print(other.tag);
        if (other.CompareTag("Safe") || other.CompareTag("Red"))
        {
            print(other.tag);

            ChangeDirection();
        }
    }


    public void ChangeDirection()
    {
        direction = direction * -1;

    }




}
