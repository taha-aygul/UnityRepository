
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{

    private Rigidbody[] _myChilds;
    public Vector3 direction;
    public float upForce, multiplier;

    // Start is called before the first frame update
    void Start()
    {
        _myChilds = gameObject.GetComponentsInChildren<Rigidbody>();
    }



    public void ThrowPieces()
    {
        // parent.transform.DetachChildren();

        for (int i = 0; i < _myChilds.Length; i++)
        {

            direction = _myChilds[i].transform.eulerAngles;
            direction.x = -Mathf.Cos(direction.y);
            direction.z = -Mathf.Sin(direction.y);
            direction.y = upForce;
            //print(direction + " " + i);

            _myChilds[i].transform.parent = null;

            Destroy(_myChilds[i].GetComponent<MeshCollider>());
            _myChilds[i].isKinematic = false;
            _myChilds[i].AddRelativeForce(direction * multiplier);
            _myChilds[i].AddTorque(direction.y* Vector3.up * multiplier);

            Destroy(_myChilds[i].gameObject, 5f);
            Destroy(gameObject, 5.1f);
            //power.x = piece.transform.eulerAngles.y;
            //piece.transform.DetachChildren();
            //piece.GetComponent<Rigidbody>().isKinematic = false;
            //piece.GetComponent<MeshCollider>().isTrigger = true;
            //piece.transform.Translate(power*circleForce, relativeTo);

        }


    }
}
