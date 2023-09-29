using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [SerializeField] private float maxDistanceToPlayer = 100;
    [SerializeField] private Vector2 rangeSpawnDistance, rangeScale;
    private float distance;


    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        if (distance > maxDistanceToPlayer)
        {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        transform.localScale = Vector3.one * Random.Range(rangeScale.x, rangeScale.y);
        transform.position = PlayerController.Instance.transform.position + (Vector3)(Random.insideUnitCircle.normalized * Random.Range(rangeSpawnDistance.x, rangeSpawnDistance.y));
    }


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") || coll.CompareTag("Star"))
        {
            UpdateDisplay();
        }
    }


}
