using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Board board;
    private void Start()
    {
        NewGame();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        board.ClearBoard();
        board.CreateTile();
        board.enabled = true;
    }


    public void GameOver()
    {
        board.enabled = false;
        print("Game Over");
        //TODO UI

    }
}
