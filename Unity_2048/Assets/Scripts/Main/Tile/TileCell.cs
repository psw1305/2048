using UnityEngine;

public class TileCell : MonoBehaviour
{
    #region Properties

    public Vector3Int Coordinate { get; set; }
    public Tile Tile { get; set; }
    public bool IsOccupied => Tile != null;

    #endregion
}
