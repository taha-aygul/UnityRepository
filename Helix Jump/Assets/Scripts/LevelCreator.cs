using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelCreator : MonoBehaviour
{

    public GameObject parentOfCircles;
    [Range(0, 12)] public int emptyCount;
    public bool hasRedPieces;
    [Range(0, 12)] public int redCount;
    public bool hasMovingPieces;
    [Range(0, 10)] public int movingPieceFrequency;
    public Material redPieceMaterial;
    public Material finalPieceMaterial;
    public Material normalPieceMaterial;



    [ContextMenu("CreateLevel")]
    public void CreateLevel()
    {
        CreateSimple();
    }




    private void CreateSimple()
    {
        for (int i = 0; i < parentOfCircles.transform.childCount ; i++)
        {
            Transform[] _pieces = parentOfCircles.transform.GetChild(i).GetComponentsInChildren<Transform>();

            int index = Random.Range(1, _pieces.Length);

            // Giving materials automatically to all pieces in a circle
            for (int j = 1; j < _pieces.Length; j++)
            {
                _pieces[j].gameObject.GetComponent<MeshRenderer>().material = normalPieceMaterial;
            }

            // Selecting empty pieces
            for (int j = 0; j < emptyCount; j++)
            {

                _pieces[index].gameObject.SetActive(false);
                index++;
                if (index >= _pieces.Length)
                {
                    index = 1;
                }

                // Setting Moving Pieces
                if (hasMovingPieces && i % movingPieceFrequency == 0)
                {

                    if (index == emptyCount + (index / 2))
                    {
                        _pieces[index].gameObject.SetActive(true);
                        _pieces[index].gameObject.AddComponent<PieceMovement>();
                    }
                }
            }
            if (hasRedPieces)
            {
                RedPieceSelect(_pieces);
            }

            // Last one is Final
            if (parentOfCircles.transform.GetChild(i).transform.position.y == 0)
            {
                for (int j = 1; j < _pieces.Length; j++)
                {
                    _pieces[j].gameObject.SetActive(true);
                    _pieces[j].gameObject.GetComponent<MeshRenderer>().material = finalPieceMaterial;
                    _pieces[j].gameObject.name = "Finish";
                    _pieces[j].gameObject.tag = "Finish";
                }
            }

        }
    }



    private void RedPieceSelect(Transform[] _pieces)
    {
        int index = Random.Range(1, _pieces.Length);

        if (!_pieces[index].gameObject.activeInHierarchy)
        {
            RedPieceSelect(_pieces);
        }

        _pieces[index].gameObject.GetComponent<MeshRenderer>().material = redPieceMaterial;
        _pieces[index].gameObject.name = "Red";
        _pieces[index].gameObject.tag = "Red";

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

