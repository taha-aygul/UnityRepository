using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCreator : MonoBehaviour
{

    [SerializeField] private GameObject cylinder;
    [SerializeField] private GameObject circlePrefab;

    public int circleCount;     // Count of Circles
    [SerializeField] private int maxCircleCount;     // Count of Circles

    [SerializeField] private int circleOffset;     // Count of Circles

    [Range(1, 12)] public int minEmptyCount;
    [Range(1, 12)] public int maxEmptyCount;
    public bool hasRedPieces;
    [Range(0, 12)] public int redCount;
    public bool hasMovingPieces;
    [Range(0, 10)] public int movingPieceFrequency;
    public float movingPieceRotatingFactor;
    public Material redPieceMaterial;
    public Material finalPieceMaterial;
    public Material normalPieceMaterial;
    //[SerializeField] private GameObject levelText;
    //[SerializeField] private Vector3 levelTextPos;
    private float point = 0;
    public float emptyCount;

    [Header("Level Tresholds")]
    [SerializeField] int emptyCountDecrease;
    [SerializeField] int redPieceActivateLevel;
    [SerializeField] int redPieceIncreaser, redPieceMaxBorder;
    [SerializeField] int movingPieceActivateLevel;
    [SerializeField] int movingPieceFrequencyIncrease, movingPieceFrequencyMaxBorder;





    public static LevelCreator Instance;


    private void Awake()
    {
        MakeSingleton();
    }

   


    private void ModifyLevel()
    {
        if (UIManager.Instance.currentLevel == 0)
        {
            return;
        }
        if (circleCount < maxCircleCount)
        {
            circleCount++;
        }
        if (UIManager.Instance.currentLevel >= redPieceActivateLevel && !hasRedPieces)
        {
            hasRedPieces = true;
            redCount = 1;
        }
        else if (UIManager.Instance.currentLevel >= movingPieceActivateLevel && !hasMovingPieces)
        {
            hasMovingPieces = true;
            movingPieceFrequency = 1;
        }

        if (UIManager.Instance.currentLevel % emptyCountDecrease == 0 && maxEmptyCount > minEmptyCount)
        {
            maxEmptyCount--;
        }
        if (hasRedPieces && UIManager.Instance.currentLevel % redPieceIncreaser == 0 && redCount <= redPieceMaxBorder)  // dikkat
        {
            redCount++;
        }
        if (hasMovingPieces && UIManager.Instance.currentLevel % emptyCountDecrease == 0 && movingPieceFrequency <= movingPieceFrequencyMaxBorder) // dikakt
        {
            movingPieceFrequency++;
        }




    }








    [ContextMenu("CreateLevel")]
    public void CreateLevel()
    {

        GameObject parenOfCircles = new GameObject()
        {
            transform = { name = "ParentOfCircles" }
        };

        ModifyLevel();

        for (int i = 0; i < circleCount; i++)
        {


            GameObject circle = Instantiate(circlePrefab, parenOfCircles.transform);
            circle.SetActive(true);
            circle.transform.SetParent(null);
            circle.transform.position = new Vector3(0, point - circleOffset * i, 0);
            circle.transform.SetParent(parenOfCircles.transform);

            if (i == circleCount - 1)   // sonuncu parçaya Final tagi verilir
            {
                circle.SetActive(true);
                circle.tag = "Final";
                circle.name = "Final";
            }
            else if (i == 2)
            {
                circle.tag = "LevelSpawn";
                circle.name = "LevelSpawn";
            }
        }
        point -= circleCount * circleOffset;


        for (int i = 0; i < parenOfCircles.transform.childCount; i++)
        {
            Transform[] _pieces = parenOfCircles.transform.GetChild(i).GetComponentsInChildren<Transform>();


            // Giving materials automatically to all pieces in a circle
            for (int j = 1; j < _pieces.Length; j++)
            {
                if (parenOfCircles.transform.GetChild(i).CompareTag("Final"))
                {

                    parenOfCircles.transform.GetChild(i).GetComponent<TextMeshPro>().text = "LEVEL " + (UIManager.Instance.currentLevel + 2);
                    _pieces[j].gameObject.GetComponent<MeshRenderer>().material = normalPieceMaterial;
                    // yazý rengi topunki ile ayný olaar
                    //parenOfCircles.transform.GetChild(i).GetComponent<TextMeshPro>().color = BallController.Instance.GetComponent<MeshRenderer>().material.color;
                }
                else
                {
                    _pieces[j].gameObject.GetComponent<MeshRenderer>().material = normalPieceMaterial;
                }

            }


            int index = Random.Range(1, _pieces.Length);
            int movePossibility = Random.Range(0, 12);
            emptyCount = Random.Range(minEmptyCount, maxEmptyCount);


            // Setting Moving Pieces
            if (hasMovingPieces && movePossibility < movingPieceFrequency && emptyCount > 2)
            {
                _pieces[index].transform.localScale = new Vector3(_pieces[index].transform.localScale.x, _pieces[index].transform.localScale.y * 1.1f, _pieces[index].transform.localScale.z * 1.1f);
                _pieces[index].transform.position = new Vector3(_pieces[index].transform.position.x, _pieces[index].transform.position.y + .2f, _pieces[index].transform.position.z);

                _pieces[index].gameObject.GetComponent<MeshRenderer>().material = redPieceMaterial;
                _pieces[index].gameObject.tag = "Red";
                _pieces[index].gameObject.SetActive(true);
                _pieces[index].gameObject.name = "MovingPiece [" + index + "]";
                _pieces[index].gameObject.AddComponent<PieceMovement>();
                _pieces[index].GetComponent<PieceMovement>().rotateAngle = 30 * emptyCount;
                _pieces[index].GetComponent<PieceMovement>().rotatingFactor = movingPieceRotatingFactor;
                _pieces[index].GetComponent<PieceMovement>().startIndex = index;
                _pieces[index].GetComponent<PieceMovement>().endIndex = index + emptyCount - 1;

            }

            // Selecting empty pieces
            for (int j = 0; j < emptyCount; j++)
            {

                index++;
                if (index >= _pieces.Length)
                {
                    index = 1;
                }
                _pieces[index].gameObject.SetActive(false);


            }
            if (hasRedPieces)
            {
                RedPieceSelect(_pieces);
            }



        }

        parenOfCircles.transform.SetParent(cylinder.transform);
    }



    private void RedPieceSelect(Transform[] _pieces)
    {
        for (int i = 0; i < redCount; i++)
        {
            int index = Random.Range(1, _pieces.Length);

            while (!_pieces[index].gameObject.activeInHierarchy)
            {
                index = Random.Range(1, _pieces.Length);
            }

            _pieces[index].gameObject.GetComponent<MeshRenderer>().material = redPieceMaterial;
            _pieces[index].gameObject.name = "Red";
            _pieces[index].gameObject.tag = "Red";
        }

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


    public void ResetLevel()
    {
        cylinder.transform.rotation = Quaternion.Euler(0, 0, 0);
        Transform[] circles = cylinder.GetComponentsInChildren<Transform>();
        foreach (var circle in circles)
        {
            if (circle.name.Equals("ParentOfCircles"))
            {
                Destroy(circle.gameObject);
            }
        }
        point = 0;
    }




    /* private void CreateSimple()
     {
         _circles = parentOfCircles.GetComponentsInChildren<Transform>();
         print(_circles.Length);

         for (int i = 0; i < _circles.Length - 1; i++)
         {
             Transform[] _pieces = _circles[i].GetComponentsInChildren<Transform>();

             print("i "+i+ "__lenght "+_pieces.Length);
             int index = Random.Range(0, _pieces.Length);

             for (int j = 0; j < emptyCount; j++)
             {
                 _pieces[index].gameObject.SetActive(false);
                 index++;
                 if (index > _pieces.Length)
                 {
                     index = 0;
                 }
             }





         }*/







}

