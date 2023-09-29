using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float
        rotationX, rotationY,
        cameraBorderX, cameraBorderY,
        sensX, sensY;

    private GameObject player;

    public float horizontalSpeed = 1f;
    // vertical rotation speed
    public float verticalSpeed = 1f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    private void Start()
    {
        player = transform.parent.gameObject;
        Cursor.visible = false;

    }
    // Update is called once per frame
    void Update()
    {
        //OnMouseDrag();
        if (Time.timeScale == 1)
        {
            RotateCamera();

        }

    }


    private void RotateCamera()
    {

        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        /* cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
         player.transform.eulerAngles = new Vector3(0f, yRotation, 0f);*/

        transform.localRotation = Quaternion.Euler(new Vector3(xRotation, 0, 0));   // Local  Rotation
        player.transform.rotation = Quaternion.Euler(new Vector3(0, yRotation, 0));
    }


    private void OnMouseDrag()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        rotationX += mouseX;
        //rotationX = Mathf.Clamp(rotationX, -cameraBorderX, cameraBorderX);
        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, -cameraBorderY, cameraBorderY);
        transform.eulerAngles = new Vector3(rotationY, rotationX, 0f);
        // player.transform.eulerAngles = new Vector3(0f, rotationX, 0f);

    }
}
