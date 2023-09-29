using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    public bool isHead;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Box") && isHead)
        {
            TailController.Instance.Delete();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            TailController.Instance.GrowSnake(other.GetComponent<Food>().foodNumber);

        }
    }
}
