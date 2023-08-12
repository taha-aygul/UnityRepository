using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;
    public GameManager gameManager;
    public CoinTrigger CoinTrigger;


    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            FindObjectOfType<Score>().WriteGameOver();
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }

        /*else if (collisionInfo.collider.tag == "Coin") 
        {
            CoinTrigger.coin.SetActive(false);
            Score += 10;
            Debug.Log(Score);
        }*/
       
      /* if (collisionInfo.collider.tag == "Wall")
        {

            float z = movement.rb.velocity.z;
            movement.rb.AddForce(0,0,-z/2,ForceMode.VelocityChange);
        }*/
         if (collisionInfo.collider.tag == "Ground" || collisionInfo.collider.tag == "Wall")
        {
            
            movement.enabled = true;
        }
        else
        {
            movement.enabled = false;

        }

        /*if (collisionInfo.collider.tag == "Finish")                                      // Oyun sonunu tag ile yapmýþtým trigger ile deil collision ile
        {
            FindObjectOfType<GameManager>().CompleteLevel();

            FindObjectOfType<Score>().WriteFinish();
            movement.Stop();
            movement.enabled = false;
        }*/

    }


}
