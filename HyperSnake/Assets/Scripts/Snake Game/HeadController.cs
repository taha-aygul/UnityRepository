using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private float currentSpeed;
    [SerializeField] private float lerpMultiplier;
    private bool IsHit { get; set; }
    private GameObject currentObstacle;


    public static HeadController Instance;

    public float CurrentSpeed { get => currentSpeed; set => currentSpeed = value; }
    public float Speed { get => speed; set => speed = value; }

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
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {

        MoveHead();

    }


    public void MoveHead()
    {
        if (currentObstacle != null)
        {
            if (currentObstacle.GetComponent<Block>().breakNumber != 0)
            {
                print("helo");
                rb.velocity = Vector3.zero;
                return;
            }
        }



        rb.velocity = Vector3.left * currentSpeed;

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                Vector3 pos = Vector3.Lerp(transform.position, hit.point, lerpMultiplier);
                transform.position = new Vector3(transform.position.x, transform.position.y, pos.z);
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            IsHit = true;
           currentObstacle = other.gameObject;
        }
        if (other.CompareTag("Food"))
        {
            TailController.Instance.GrowSnake(other.GetComponent<Food>().foodNumber);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            IsHit = false;
            //            currentSpeed = speed;

        }
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Box"))
        {

            IsHit = true;
            currentSpeed = 0;
        }
        

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.collider.CompareTag("Box"))
        {
            IsHit = false;
            currentSpeed = speed;

        }

    }*/



}
