using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailController : MonoBehaviour
{

    public float MoveSpeed = 5;
    public float SteerSpeed = 180;
    public float BodySpeed = 5;
    public int Gap = 10;
    public float step;
    // References
    public GameObject BodyPrefab;

    // Lists
    private List<GameObject> BodyParts = new List<GameObject>();
    private Queue<GameObject> segments = new Queue<GameObject>();
    private List<Vector3> PositionsHistory = new List<Vector3>();
    private Transform lastSegment;
    private int totalTailCount = 0;

    

    public static TailController Instance;

    private void Awake()
    {
        MakeSingleton();
    }

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        lastSegment = transform;
        GrowSnake();
        GrowSnake();
    }


    // Update is called once per frame
    void Update()
    {
        // Store position history
        PositionsHistory.Insert(0, transform.position);
       
    }
    private void FixedUpdate()
    {
        MoveTail();
    }


    private void MoveTail()
    {
        if (segments.Count == 0)
        {
            print("Boþ yýlan");      // TODO GameOver
            Time.timeScale = 0;
            return;
        }
        segments.Peek().GetComponent<SegmentController>().isHead = true;

        int index = 1;
        foreach (var body in segments)
        {
            Vector3 point = PositionsHistory[Mathf.Clamp(index * Gap, 0, PositionsHistory.Count - 1)];

            Vector3 moveDirection = point - body.transform.position;
            //body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
             body.transform.Translate(   moveDirection * BodySpeed * Time.deltaTime);
            //body.transform.position = Vector3.MoveTowards( transform.position,point, step);
           // body.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(transform.position , point , step));
            //body.transform.LookAt(point);
            index++;
        }

        /*for (int i = 0; i < segments.Count; i++)
        {
            GameObject body = segments.Peek();
            Vector3 point = PositionsHistory[Mathf.Clamp(i * Gap, 0, PositionsHistory.Count - 1)];

            // Move body towards the point along the snakes path
            Vector3 moveDirection = point - body.transform.position;
            body.transform.position += moveDirection * BodySpeed * Time.deltaTime;
           // body.transform.Translate(point);
            // Rotate body towards the point along the snakes path
            body.transform.LookAt(point);


            segments.Enqueue(segments.Dequeue());

        }*/
       


    }
    [ContextMenu("GrowSnake")]
    public void GrowSnake()
    {
        lastSegment.GetComponent<Transform>().name = "segment";
        transform.name = "Head [" + segments.Count +"]";
        GameObject body = Instantiate(BodyPrefab, new Vector3 (lastSegment.position.x + Gap , transform.position.y , lastSegment.position.z), Quaternion.identity );
        segments.Enqueue(body);
        lastSegment = body.transform;
        lastSegment.GetComponent<Transform>().name = "last";
        segments.Peek().GetComponent<SegmentController>().isHead = true;
        totalTailCount++;
        
    }


    [ContextMenu("GrowSnake")]
    public void GrowSnake(int growCount)
    {
        for (int i = 0; i < growCount; i++)
        {
            GrowSnake();
        }
    }

    public void Delete()
    {
        GameObject last = segments.Peek().gameObject;
        segments.Dequeue();
        Destroy(last);

    }

    public int CurrentTailCount()
    {
        return segments.Count;
    }
    public int TotalTailCount()
    {
        return totalTailCount;
    }


}
