using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAndLevelCreator : MonoBehaviour
{
    public int emptyPiecesCount; // Constant empty  pieces
    public int minEmptyPiecesCount;
    public int maxEmptyPiecesCount;
    private Transform[] _pieces;
    public Material Material_1;
    public bool redPieces;
    public int redPiecesCount;
    public bool movingPieces;
    public int movingPieces_Count;





    #region ContextMenu



    [ContextMenu(nameof(CreateSimpleLevel))]

    public void CreateSimpleLevel()
    {
        SimpleLevel();
    }
    [ContextMenu(nameof(CreateMiddleLevel))]

    public void CreateMiddleLevel()
    {
        MiddleLevel();
    }
    [ContextMenu(nameof(CreateHardLevel))]

    public void CreateHardLevel()
    {
       HardLevel();
    }

    [ContextMenu(nameof(CreateManuelLevel))]

    public void CreateManuelLevel()
    {
        ManuelLevel();
    }

    [ContextMenu(nameof(ResetLevel))]

    public void ResetLevel()
    {
        ResLevel();
    }
    #endregion




    public void SimpleLevel()
    {
        minEmptyPiecesCount = 4;
        maxEmptyPiecesCount = 6;
       
            _pieces = GetComponentsInChildren<Transform>();

            int emptyCount = Random.Range(minEmptyPiecesCount, maxEmptyPiecesCount);
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


            //circlePrefab.transform.position = new Vector3(0, circleOffsetVertical * i, 0);
     }



    public void MiddleLevel()
    {
        minEmptyPiecesCount = 3;
        maxEmptyPiecesCount = 5;

        _pieces = GetComponentsInChildren<Transform>();

        int emptyCount = Random.Range(minEmptyPiecesCount, maxEmptyPiecesCount);
        int index = Random.Range(1, _pieces.Length);

        for (int j = 0; j < emptyCount; j++)
        {


            if (_pieces[index].CompareTag("Circle"))
            {

            }
            else
            {
                _pieces[index].gameObject.SetActive(false);

            }
            index++;
            if (index > _pieces.Length-1)
            {
                index = 0;
            }
        }
        RandomPieceSelect();

        //circlePrefab.transform.position = new Vector3(0, circleOffsetVertical * i, 0);
    }

    private void RandomPieceSelect()
    {
        int index = Random.Range(0, _pieces.Length);

        if (_pieces[index].gameObject.activeInHierarchy)
        {
            if (_pieces[index].CompareTag("Circle"))
            {

            }
            else
            {
                _pieces[index].gameObject.GetComponent<MeshRenderer>().material = Material_1;
                _pieces[index].gameObject.name = "Red";
                _pieces[index].gameObject.tag = "Red";
            }
            

        }

    }



    public void HardLevel()
    {
        minEmptyPiecesCount = 3;
        maxEmptyPiecesCount = 5;

        _pieces = GetComponentsInChildren<Transform>();

        int emptyCount = Random.Range(minEmptyPiecesCount, maxEmptyPiecesCount);
        int index = Random.Range(0, _pieces.Length);

        for (int j = 0; j < emptyCount; j++)
        {

            if (index == emptyCount + (index / 2)) 
            {
                _pieces[index].gameObject.AddComponent<PieceMovement>();
            }
            else
            {
                _pieces[index].gameObject.SetActive(false);
            }

            index++;
            if (index > _pieces.Length-1)
            {
                index = 0;
            }
        }
        RandomPieceSelect();


        //circlePrefab.transform.position = new Vector3(0, circleOffsetVertical * i, 0);
    }






    public void ManuelLevel()
    {
        _pieces = GetComponentsInChildren<Transform>();


        int emptyCount = Random.Range(minEmptyPiecesCount, maxEmptyPiecesCount);
        int index = Random.Range(0, _pieces.Length);
        int emptyStartingIndex = index;
        for (int j = 0; j < emptyCount; j++)
        {

            if(movingPieces && index == emptyCount/2 + emptyStartingIndex)
            {
                _pieces[index].gameObject.AddComponent<PieceMovement>();
            }
            else
            {
                _pieces[index].gameObject.SetActive(false);
            }

            index++;
            if (index > _pieces.Length-1)
            {
                index = 0;
            }
        }


        if (redPieces)
        {
            for (int i = 0; i < redPiecesCount; i++)
            {
                RandomPieceSelect();
            }
        }
       

        //circlePrefab.transform.position = new Vector3(0, circleOffsetVertical * i, 0);
    }


    public void ResLevel()
    {
        _pieces = GetComponentsInChildren<Transform>();

        for (int i = 0; i < _pieces.Length; i++)
        {
            _pieces[i].gameObject.SetActive(true);

        }

    }



}
