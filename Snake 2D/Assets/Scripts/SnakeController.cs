using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    [SerializeField] private Vector2 direction = Vector2.zero;
    public Transform snakeTailPr;
    [SerializeField] private List<Transform> snakeSegments;
    private bool _isAlive;

    private void Start()
    {
        _isAlive = true;
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
    }

    void Update()
    {
        InputHandler();
        if (_isAlive)
        {
            GameManager.Instance.UpdateTime();
        }
       
    }

    private void FixedUpdate()                   // Hareketi Fixed Updatede yaptým ve yavaþ hareket etmesi için Edit/Project Settings/ Time / Fixed Time arttýrabilirsin 
    {
        Move();
    }

    private void Move()
    {
        if (_isAlive)
        {
            for (int i = snakeSegments.Count - 1; i > 0; i--)                       // FOLLOWER MOVEMENT WÝTH LÝST
            {
                snakeSegments[i].position = snakeSegments[i - 1].position;
            }
        }
        else
        {
            direction = Vector2.zero;
        }

        transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x + direction.x), Mathf.Round(transform.localPosition.y + direction.y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
            GameManager.Instance.UpdateScore(1);

        }
        if (collision.CompareTag("Obstacle"))
        {
            _isAlive = false;
            GameManager.Instance.GameOver();

        }
    }



    private void InputHandler()
    {
        if (direction != Vector2.left && direction != Vector2.right)
        {
            if (Input.GetKey(KeyCode.A))
                direction = Vector2.left;
            if (Input.GetKey(KeyCode.D))
                direction = Vector2.right;
        }

        if (direction != Vector2.up && direction != Vector2.down)
        {
            if (Input.GetKey(KeyCode.W))
                direction = Vector2.up;
            if (Input.GetKey(KeyCode.S))
                direction = Vector2.down;
        }


    }

    private void Grow()
    {
        GameObject snakeTail = Instantiate(snakeTailPr.gameObject);
        snakeTail.transform.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(snakeTail.transform);

    }

}
