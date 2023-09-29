using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    private Vector2 _touchPosition;
    public Vector2 Direction { get; private set; }
    [SerializeField] private Image pivotImage;
    // Start is called before the first frame update
    
    public void OnDrag(PointerEventData eventData)
    {
        Direction = -(eventData.position - _touchPosition).normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pivotImage.enabled = true;
        pivotImage.transform.position = eventData.position;
        _touchPosition = eventData.position;
        transform.position = eventData.position;

    }


    // Not using
    public void OnPointerUp(PointerEventData eventData)
    {
        pivotImage.enabled = false;
        Direction = Vector3.zero;
    }


}
