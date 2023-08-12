using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    [SerializeField] private Vector2 direction = Vector2.zero;
    public Transform snakeTailPr;
    [SerializeField] private List<Transform> snakeSegments;

    private void Start()
    {
        snakeSegments = new List<Transform>();
        snakeSegments.Add(this.transform);
        Grow();
    }

    void Update()
    {
        InputHandler();
    }

    private void FixedUpdate()                   // Hareketi Fixed Updatede yaptým ve yavaþ hareket etmesi için Edit/Project Settings/ Time / Fixed Time arttýrabilirsin 
    {
        Move();
    }

    private void Move()
    {
       for (int i = snakeSegments.Count-1; i >0; i--)                       // FOLLOWER MOVEMENT WÝTH LÝST
        {
            snakeSegments[i].position = snakeSegments[i - 1].position;
        }
        transform.localPosition = new Vector3(Mathf.Round(transform.localPosition.x + direction.x) , Mathf.Round(transform.localPosition.y + direction.y), 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") && collision.collider.CompareTag("Tail"))
        {
            print(  "GA");
            // TODO GAMEOVER UI
        }
    }

    private void InputHandler()
    {
        if (direction != Vector2.left && direction != Vector2.right )
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
        snakeTail.transform.position = snakeSegments[snakeSegments.Count-1].position;
        snakeSegments.Add(snakeTail.transform);

    }

}
