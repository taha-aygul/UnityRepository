using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleManager : MonoBehaviour
{
    
    [SerializeField] GameObject piecePrefab;
    [SerializeField] GameObject circlePrefab;

    public float pieceAmount, fullAngle;
    public float circleOffsetVertical;
    public int createAmount;     // Count of Circles
    public int emptyPiecesCount; // Constant empty  pieces
    public int minEmptyPiecesCount; 
    public int maxEmptyPiecesCount;
    public float  circleForce;
    public Vector3 translation;
    public Space relativeTo;

    private Transform[] _pieces;



    public static CircleManager Instance;

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

    #region ContextMenu

   /* [ContextMenu(nameof(CreateCirclesFromPieces))]

    public void CreateCirclesFromPieces()
    {
        CreateCircles(createAmount);
    }*/

    [ContextMenu(nameof(CreateCircles))]

    public void CreateCircles()
    {
        CreateCirclesAndParentOfCircles(createAmount);
    }

    [ContextMenu(nameof(CreateCircle))]

    public void CreateCircle()
    {
        CreateCircleParentFromPieces();
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
       // Create(createAmount);
    }

    /// <summary>
    /// Creates one Circle Parent by using piecePrefab
    /// </summary>
    /// <returns> Circle Parent object </returns>
    public GameObject CreateCircleParentFromPieces()
    {
        //  GameObject parent = new GameObject(transform.name = "Parent");  // yarattýðým kendi objemin adýný deðiþtirme

        GameObject parent = new GameObject()  // bu yapý ve34. satýrdakþ kýsým yaratýlan objeleri bir parent'a koyar
        {
            transform={ name = "CircleParent" }
        };

        for (int i =0; i < pieceAmount; i++) 
        {
            //  GameObject curre = Instantiate(piecePrefab, parent.transform, false);

            GameObject current = Instantiate(piecePrefab, parent.transform,true);       // burayý incele
            Quaternion rotation =current.transform.rotation;
            rotation.eulerAngles = new Vector3(0,(fullAngle/pieceAmount)*i,0);
            current.transform.rotation = rotation;
        }
        return parent;
    }







    /// <summary>
    /// Creates Cirles with circleOffsetVertical by using the return parameter of CreateCircleParentFromPieces
    /// </summary>
    /// <param name="createAmount"></param>
    public void CreateCircles(int createAmount)
    {
        for (int i = 0; i < createAmount; i++)
        {
            GameObject circle = CreateCircleParentFromPieces();
            circle.transform.position = new Vector3(0,circleOffsetVertical*i,0);
        }
    }


    /// <summary>
    /// Instantiates Circles  with circleOffsetVertical by using circlePrefab
    /// </summary>
    /// <param name="createAmount"></param>
    public void CreateCirclesAndParentOfCircles(int createAmount)
    {
        GameObject parenOfCircles = new GameObject()  // bu yapý ve34. satýrdakþ kýsýn yaratýlan objeleri bir parent a koyar
        {
            transform = { name = "ParentOfCircles" }
        };
        for (int i = 0; i < createAmount; i++)
        {
            
            Instantiate(circlePrefab,parenOfCircles.transform,true);
          
            circlePrefab.SetActive(true);
            circlePrefab.transform.position = new Vector3(0, circleOffsetVertical * i, 0);
        }
    }



    public void ThrowPieces(GameObject parent)
    {
        _pieces = parent.GetComponentsInChildren<Transform>();
       // parent.transform.DetachChildren();
        Vector3 power = translation;

        for (int i = 1; i < _pieces.Length; i++)
        {
            _pieces[i].transform.parent = null;

            //GameObject piece = _pieces[i].gameObject;
            //power.x = piece.transform.eulerAngles.y;
            //piece.transform.DetachChildren();
            //piece.GetComponent<Rigidbody>().isKinematic = false;
            //piece.GetComponent<MeshCollider>().isTrigger = true;
            //piece.transform.Translate(power*circleForce, relativeTo);

            //piece.GetComponent<Rigidbody>().AddForce(circleForce *new Vector3(1,1,1));
        }


    }













}
