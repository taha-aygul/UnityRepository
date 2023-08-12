using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public Emove enemyMovement;
    public GameObject gameOverScreen;
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {

        if(collisionInfo.collider.tag == "Player")
        {
            gameOverScreen.SetActive(true);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "wall")
        {
            enemyMovement.changeDirection();
        }
    }

}
