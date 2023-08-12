using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameManager gameManager;
    public Tile tilePrefab;
    public TileState[] tileStates;
    private Grid grid;
    private List<Tile> tiles;
    private bool waitingForMove;


    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        tiles = new List<Tile>();
    }

   /* private void Start()
    {
        CreateTile();
        CreateTile();
    }*/

    public void ClearBoard()
    {
        foreach (var cell in grid.cells)
        {
            cell.tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }
        tiles.Clear();

    }
    public void CreateTile()
    {
        Tile tile = Instantiate(tilePrefab, grid.transform);

        float random = UnityEngine.Random.Range(0f, 1f);

        if (random < 0.7f)
        {
            tile.SetState(tileStates[0], 2);
            tile.Spawn(grid.GetRandomEmptyCell());
        }
        else
        {
            tile.SetState(tileStates[1], 4);
            tile.Spawn(grid.GetRandomEmptyCell());

        }
    }


    private void Update()
    {
        if (!waitingForMove)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(Vector2Int.up, 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2Int.left, 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
            }
        }
    }

    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        bool changed = false;
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX)
        {

            for (int y = startY; y >= 0 && y < grid.width; y += incrementY)
            {
                Cell cell = grid.GetCell(x, y);
                if (cell.occupied)
                {
                    changed |=MoveTile(cell.tile, direction);       // bir kez true olduktan sonra burdan false döndüremez  |=               &=
                }
            }
        }
        if (changed)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector2Int direction)
    {
        Cell newCell = null;
        Cell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if(CanMerge(tile, adjacent.tile))
                {
                    Merge(tile, adjacent.tile);
                    return true;
                }
                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }


        if (newCell != null)
        {
            tile.MoveTO(newCell);
            return true;
        }

        return false;
    }

    private void Merge(Tile tile1, Tile tile2)
    {
        tiles.Remove(tile1);
        tile1.Merge(tile2.cell);
        int nextStateIndex =  Mathf.Clamp( IndexOf(tile2.state) + 1,0,tileStates.Length-1);
        int number = tile2.number * 2;
        tile2.SetState(tileStates[nextStateIndex], number);
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.state== b.state && !b.isMergedThisRound;
    }


    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i]);
            { return i; }
        }
        return -1;
    }


    private IEnumerator WaitForChanges()
    {
        waitingForMove = true;

        yield return new WaitForSeconds(0.1f);
        waitingForMove = false;


        foreach (var tile in tiles)
        {
            tile.isMergedThisRound = false;
        }
        if(tiles.Count != grid.size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            gameManager.GameOver();
        }

    }


    private bool CheckForGameOver()
    {
        if(tiles.Count != grid.size)
        {
            return false;
        }
         foreach (var tile in tiles)
            {
                Cell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
                Cell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
                Cell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
                Cell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

                if (up != null && CanMerge(tile, up.tile))
                {
                    return false;
                }

                if (down != null && CanMerge(tile, down.tile))
                {
                    return false;
                }

                if (left != null && CanMerge(tile, left.tile))
                {
                    return false;
                }

                if (right != null && CanMerge(tile, right.tile))
                {
                    return false;
                }
            }

            return true;

        }
}
