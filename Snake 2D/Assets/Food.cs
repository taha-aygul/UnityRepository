using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour 
{
    public BoxCollider2D spawnZone;


    private void Start()
    {
        RandomizePosition();
    }

    public void RandomizePosition() 
    {

        float x = Random.Range(spawnZone.bounds.min.x, spawnZone.bounds.max.x);
        float y = Random.Range(spawnZone.bounds.min.y, spawnZone.bounds.max.y);

        Vector2 randomPos = new Vector2(Mathf.Round(x), Mathf.Round(y));

        transform.localPosition = randomPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        RandomizePosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RandomizePosition();
    }
}
