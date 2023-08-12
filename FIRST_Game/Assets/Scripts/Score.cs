using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public Transform player;
    public PlayerMovement movement;
    public Text scoreText;
    private string score;
    bool cont = true;
    private void Update()
    {
        if (cont) 
        {
            scoreText.text = "         "+movement.rb.position.x.ToString("0");
        }
    }
    public void WriteGameOver()
    {
        cont = false;
        score = player.position.x.ToString("0");
        scoreText.text = "GAME OVER\n" +"        "+ score;
    }
    public void WriteFinish()
    {
        cont = false;
        score = player.position.x.ToString("0");
        scoreText.text = "YOU WIN\n" + "        " + score;
    }
}
