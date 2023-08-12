using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Row[] rows { get; private set; }
    public Cell[] cells { get; private set; }

    public int size, height, width;

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
        cells = GetComponentsInChildren<Cell>();

        size = cells.Length;
        height = rows.Length;
        width = size / height;

    }

    private void Start()
    {
        for (int i = 0; i < rows.Length; i++)          // Bu kodda Row larý ve Cell lere koordinatlar veriyoruz ki onlarý bu koordinatlar arasýnda hareket ettirelim
        {
            for (int j = 0; j < rows[i].cells.Length; j++)
            {
                rows[i].cells[j].coordinates = new Vector2Int(j, i);
            }

        }
    }

    public Cell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startingBorder = index;
        while (cells[index].occupied)
        {
            index++;

            if (index >= cells.Length)
                index = 0;

            if (index == startingBorder)
                return null;
        }

        return cells[index];

    }

    public Cell GetCell(int x, int y)
    {

        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            return rows[y].cells[x];
        }
        else
        {
            return null;
        }

    }

    public Cell GetAdjacentCell(Cell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates.x, coordinates.y);






    }

}
