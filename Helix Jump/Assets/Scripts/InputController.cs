using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IDragHandler
{
   /* public Vector3 mouseClickPosition, mouseDragPosition, offset;
   public bool isGameStarted;

    // Start is called before the first frame update

   void  OnMouseDown()
    {
        mouseClickPosition = Input.mousePosition;
    }

    
    private void OnMouseDrag()
    {

       if (isGameStarted)
       {
           mouseDragPosition = Input.mousePosition;
           offset = mouseClickPosition - mouseDragPosition;
           transform.eulerAngles = new Vector3(0, sensivity * offset.x, 0);
       }

       if (!isGameStarted)
       {
           UIManager.Instance.closeEntry();
           isGameStarted = true;
       }

   }*/
    

    public Rigidbody ballRb;
    [SerializeField] private Transform main;
    [SerializeField] private float sensivity;
    private bool isStarted;

    private void Start()
    {
        //ballRb.Sleep();
        ballRb.isKinematic = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isStarted)
        {
            UIManager.Instance.closeEntry();
            isStarted = true;
            //ballRb.WakeUp();
            ballRb.isKinematic = false;

            return;
        }

      //  print(eventData.delta.x);
        Quaternion rotation = main.rotation;
        float current = rotation.eulerAngles.y;
        current += -eventData.delta.x * sensivity;
        rotation.eulerAngles = new Vector3(0, current, 0);
        main.rotation = rotation;
    }
}
