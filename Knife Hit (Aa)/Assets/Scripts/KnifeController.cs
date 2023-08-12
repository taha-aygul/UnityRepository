using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{

    private Rigidbody2D knife;
    [Range(0,100)] public float speed;
    public float moveSpeed;


    private void Start()
    {
        moveSpeed = 3;
        knife = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveKnifeToThrowPosition();
        ShootInput();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Circle"))
        {
            knife.isKinematic = true;
            transform.SetParent(collision.collider.gameObject.transform);
            KnifeManager.Instance.ActivateNextKnife();
            ScoreManager.Instance.IncreaseScore();
            UIManager.Instance.firstThrow = false;

        }
        else if (collision.collider.gameObject.CompareTag("Knife"))
        {
            knife.isKinematic = true;
            UIManager.Instance.GameFailed();
            LevelManager.Instance.ReloadCurrentLevel();
        }

    }


    private void MoveKnifeToThrowPosition()
    {
        if (transform.gameObject.activeInHierarchy && !knife.isKinematic && UIManager.Instance.isGameStarted)
        {
            knife.transform.position = Vector3.Lerp(transform.position, new Vector3(0, -2.8f, 0), moveSpeed * Time.deltaTime);
        }
    }

    private void ShootInput()
    {

        if (Input.GetMouseButtonDown(0) && UIManager.Instance.playUI.activeInHierarchy) // Left Click   && !UIManager.Instance.isGameFailed
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        knife.AddForce(Vector2.up * speed, ForceMode2D.Impulse);
    }


}
