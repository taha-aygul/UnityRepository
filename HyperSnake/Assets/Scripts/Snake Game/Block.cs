using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    public int breakNumber;
    public TextMesh numberText;
    public Color color;
   

    // Update is called once per frame
    void Update()
    {
        numberText.text = breakNumber.ToString();
    }

    public void DecreaseNumber()
    {

        breakNumber--;
        if (breakNumber <= 0)
        {
            transform.Translate(new Vector3(0,100,100));
            Destroy(gameObject,2f);
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball") && collision.collider.GetComponent<SegmentController>().isHead)
        {
            DecreaseNumber();
        }
    }
}
