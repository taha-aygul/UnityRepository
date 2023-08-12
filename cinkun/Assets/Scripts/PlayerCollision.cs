using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.collider.tag == "gem")
        {
            Rigidbody2D rb = collision.collider.gameObject.GetComponent<Rigidbody2D>();
            FindAnyObjectByType<Score>().increaseGem();
            rb.gravityScale = -0.5f;
            rb.AddForce(new Vector2(Random.Range(-5f, 5f), 0));

            collision.collider.enabled = false;

            
        }
        else if (collision.collider.tag == "cherry")
        {
            Rigidbody2D rb = collision.collider.gameObject.GetComponent<Rigidbody2D>();
            FindAnyObjectByType<Score>().increaseCherry();
            rb.gravityScale = -0.5f;
            rb.AddForce(new Vector2(Random.Range(-5f,5f),0));
            collision.collider.enabled = false;

        }
        else if(collision.collider.tag == "caveEnter")
        {
            gameManager.LoadScene("Cave");
        }
    }


}
