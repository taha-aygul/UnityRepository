using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{

    public Vector3 direction = new Vector3(0, 90, 0);
    public float rotatingFactor = 1;
    public bool manuelSelect;
    public float rotateAngle;
    public float startIndex, endIndex;

    [SerializeField] private float startAngle;
    [SerializeField] private float endAngle;


    private void Start()
    {
        startAngle = transform.eulerAngles.y;
        endAngle = transform.eulerAngles.y + rotateAngle;
        rotatingFactor = 1;

        if (endAngle> 360)
        {
            endAngle -= 360;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        RotatePiece();
    }


    public void RotatePiece()
    {
        

        if (transform.eulerAngles.y > endAngle  && transform.eulerAngles.y <= endAngle+5)
        {
            rotatingFactor *= -1;
        }
        else if (transform.eulerAngles.y < startAngle && transform.eulerAngles.y >= startAngle - 5)
        {
            rotatingFactor *= -1 ;
        }
        transform.Rotate(direction *Time.deltaTime * rotatingFactor );
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Safe") || collision.collider.CompareTag("Red"))
        {
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
