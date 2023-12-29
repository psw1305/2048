using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    #region Fields

    [SerializeField] private TileGrid grid;

    private Tile tilePrefab;
    private TileState[] tileStates;
    private List<Tile> tiles;

    private bool isWaiting;

    #endregion

    #region Init

    public void Initialize()
    {
        tilePrefab = Manager.Resource.GetObject("Tile").GetComponent<Tile>();
        tileStates = Manager.Resource.GetTiles();

        tiles = new(16);
    }

    public void ClearBoard()
    {
        foreach (var cell in grid.Cells) 
        {
            cell.Tile = null;
        }

        foreach (var tile in tiles)
        {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
    }

    public void CreateTile()
    {
        var tile = Instantiate(tilePrefab, grid.transform);

        // 90% => 2, 10% => 4
        int random = Random.Range(0, 100);
        if (random < 10)
        {
            tile.SetState(tileStates[1]);
        }
        else
        {
            tile.SetState(tileStates[0]);
        }

        tile.SpawnTile(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    #endregion

    #region MonoBehaviour

    private void Update()
    {
        if (!isWaiting)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Move(new Vector3Int(0, 0, 1), 0, 1, 1, 1);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(new Vector3Int(-1, 0, 0), 1, 1, 0, 1);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(new Vector3Int(0, 0, -1), 0, 1, grid.Height - 2, -1);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(new Vector3Int(1, 0, 0), grid.Width - 2, -1, 0, 1);
            }
        }
    }

    #endregion

    #region Input Action

    private void Move(Vector3Int direction, int startX, int incrementX, int startZ, int incrementZ)
    {
        bool isChanged = false;

        for (int x = startX; x >= 0 && x < grid.Width; x += incrementX)
        {
            for (int z = startZ; z >= 0 && z < grid.Height; z += incrementZ)
            {
                var cell = grid.GetCell(x, z);

                if (cell.IsOccupied)
                {
                    isChanged |= MoveTile(cell.Tile, direction);
                }
            }
        }

        if (isChanged)
        {
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector3Int direction)
    {
        TileCell newCell = null;
        var adjacent = grid.GetAdjacentCell(tile.Cell, direction);

        while (adjacent != null)
        {
            if (adjacent.IsOccupied)
            {
                if (CanMerge(tile, adjacent.Tile))
                {
                    MergeTiles(tile, adjacent.Tile);
                    return true;
                }

                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null)
        {
            tile.MoveToTile(newCell);
            return true;
        }

        return false;
    }

    private bool CanMerge(Tile a, Tile b)
    {
        return a.State == b.State && !b.IsLocked;
    }

    private void MergeTiles(Tile a, Tile b)
    {
        tiles.Remove(a);
        a.MergeTile(b.Cell);

        var index = Mathf.Clamp(IndexOf(b.State) + 1, 0, tileStates.Length - 1);
        var newState = tileStates[index];

        b.SetState(newState);
        Manager.Game.AddScore(newState.TileNumber);
    }

    private int IndexOf(TileState state)
    {
        for (int i = 0; i < tileStates.Length; i++)
        {
            if (state == tileStates[i])
            {
                return i;
            }
        }

        return -1;
    }

    private IEnumerator WaitForChanges()
    {
        isWaiting = true;

        yield return new WaitForSeconds(0.1f);

        isWaiting = false;

        foreach (var tile in tiles)
        {
            tile.SetLock(false);
        }

        if (tiles.Count != grid.Size)
        {
            CreateTile();
        }

        if (CheckForGameOver())
        {
            Manager.Game.GameOver();
        }
    }

    public bool CheckForGameOver()
    {
        if (tiles.Count != grid.Size)
        {
            return false;
        }

        foreach (var tile in tiles)
        {
            TileCell up = grid.GetAdjacentCell(tile.Cell, new Vector3Int(0, 0, 1));
            TileCell down = grid.GetAdjacentCell(tile.Cell, new Vector3Int(0, 0, -1));
            TileCell left = grid.GetAdjacentCell(tile.Cell, new Vector3Int(-1, 0, 0));
            TileCell right = grid.GetAdjacentCell(tile.Cell, new Vector3Int(1, 0, 0));

            if (up != null && CanMerge(tile, up.Tile))
            {
                return false;
            }

            if (down != null && CanMerge(tile, down.Tile))
            {
                return false;
            }

            if (left != null && CanMerge(tile, left.Tile))
            {
                return false;
            }

            if (right != null && CanMerge(tile, right.Tile))
            {
                return false;
            }
        }

        return true;
    }

    #endregion
}
