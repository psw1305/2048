using UnityEngine;

public class TileGrid : MonoBehaviour
{
    #region Properties

    public TileRow[] Rows { get; private set; }
    public TileCell[] Cells { get; private set; }
    public int Size => Cells.Length;
    public int Height => Rows.Length;
    public int Width => Size / Height;

    #endregion

    #region Init

    private void Awake()
    {
        Rows = GetComponentsInChildren<TileRow>();
        Cells = GetComponentsInChildren<TileCell>();

        for (int i = 0; i < Cells.Length; i++)
        {
            Cells[i].Coordinate = new Vector3Int(i % Width, 0, i / Width);
        }
    }

    public TileCell GetCell(Vector3Int coordinates)
    {
        return GetCell(coordinates.x, coordinates.z);
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            return Rows[y].Cells[x];
        }
        else 
        { 
            return null; 
        }
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector3Int direction)
    {
        var coordinates = cell.Coordinate;
        coordinates.x += direction.x;
        coordinates.z -= direction.z;

        return GetCell(coordinates);
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, Cells.Length);
        int startingIndex = index;

        while (Cells[index].IsOccupied)
        {
            index++;

            if (index >= Cells.Length)
            {
                index = 0;
            }

            if (index == startingIndex)
            {
                return null;
            }
        }

        return Cells[index];
    }

    #endregion
}
