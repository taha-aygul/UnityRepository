using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("Ball")]
    [SerializeField] private float upVelocity;
    [SerializeField] private float upVelocityBorder;

    [SerializeField] private float downVelocity;
    [SerializeField] private float minVelocity;
    [SerializeField] private Vector3 circleForce;// force, downForce; 
    public int comboBreakCount = 3;
    public int comboCount;
    [Header("Animations")]
    [SerializeField] Animator ballBounce;
    [SerializeField] private GameObject splashAnimation, splasTexture, deadAnimation, comboAnimation;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] Vector2 trailTime;
    [Header("Materials")]
    [SerializeField] Material splashMaterial;
    [SerializeField] Material ballMaterial;
    [SerializeField] AudioSource ballAudio;
    [SerializeField] AudioClip splashClip;
    private float _previousSpeed;
    private Rigidbody rb;
    public Vector3 startPosition;



    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.RandomBonuceAudio();
        splashMaterial.color = ballMaterial.color;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {

        if (rb.velocity.y <= upVelocityBorder && _previousSpeed >= upVelocityBorder)
        {
            rb.velocity = downVelocity * Vector3.down * Time.deltaTime;
        }

        if (rb.velocity.y >= 0 && _previousSpeed < 0)
        {
            rb.velocity = upVelocity * Vector3.up * Time.deltaTime;
        }

        Vector3 vertical = rb.velocity;
        vertical.y = Mathf.Clamp(vertical.y, minVelocity, upVelocity);
        rb.velocity = vertical;
        _previousSpeed = rb.velocity.y;
        //  rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

    }



    private void OnCollisionEnter(Collision collision)
    {

        ballBounce.Play("Base Layer.BallBounceAnimation", 0, 0.25f);

        GameObject splashAnim = Instantiate(splashAnimation);// rb.position, splashRotation
        splashAnim.transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 5) * 2, transform.position.z);
        Destroy(splashAnim, 1f);

        GameObject splash = Instantiate(splasTexture, collision.collider.transform);// rb.position, splashRotation
        splash.transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y / 5) * 2, transform.position.z); //collision.contacts[0].point + collision.collider.transform.localScale.y / 2 * Vector3.up;//transform.position;


        if (comboCount >= comboBreakCount)         // Çarptýðýný parçalama kýsmý
        {
            collision.collider.GetComponentInParent<CircleController>().ThrowPieces();
            SoundManager.Instance.PlayBallBounce();
            trail.time = trailTime.x;
            comboCount = 0;
            return;
        }  // if has comboPower

        if (collision.collider.CompareTag("Red"))
        {
            Instantiate(deadAnimation, rb.position, Quaternion.identity);
            GetComponent<MeshRenderer>().forceRenderingOff = true;

            rb.isKinematic = true;
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Invoke("CallOpenDeadUI", 0.3f);
            Invoke("CallRestart", 2f);
        }
        else if (collision.collider.CompareTag("Safe"))
        {
            SoundManager.Instance.PlayBallBounce();
        }

        comboCount = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Final"))
        {
            comboCount++;
            Destroy(other.transform.parent.gameObject, 5f);
           
            UIManager.Instance.UpdateLevelAtStart();

            if (UIManager.Instance.currentLevel % ColorManager.Instance.colorChageLevelFreq == 0 && UIManager.Instance.currentLevel != 0)
            {
                ColorManager.Instance.RandomIndex();
                ColorManager.Instance.NextTheme();
            }
            UIManager.Instance.Checkpoint();

            UIManager.Instance.UpdateLevelTextAndColor();


        }
        else if (other.CompareTag("LevelSpawn"))
        {
            comboCount++;
            LevelCreator.Instance.CreateLevel();
        }
        else if (other.CompareTag("Circle"))
        {
            // other.GetComponent<CircleController>().ThrowPieces();
            comboCount++;

            if (comboCount >= comboBreakCount)
            {
                GameObject jump = Instantiate(comboAnimation, rb.position, Quaternion.identity);
                trail.time = trailTime.y;
                Destroy(jump, 1f);
            }


        }
    }


    private void CallRestart()
    {
        UIManager.Instance.Restart();
    }

    private void CallOpenDeadUI()
    {
        UIManager.Instance.openDeadUI();
    }

    public void ActivateBall()
    {
        GetComponent<MeshRenderer>().forceRenderingOff = false;

    }
    public void MoveBallToStart()
    {
        transform.position = startPosition;
    }

}
