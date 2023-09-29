using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int foodNumber;
    public TextMesh numberText;
    public Color color;

    void Update()
    {
        numberText.text = foodNumber.ToString();
    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && collision.collider.GetComponent<SegmentController>().isHead)
        {
            transform.Translate(new Vector3(0, 100, 100));
            Destroy(gameObject, 2f);
        }
    }
}
