using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Blueprint/Tile")]
public class TileState : ScriptableObject
{
    [SerializeField] private int tileNumber;
    [SerializeField] private Color tileColor;

    public int TileNumber => tileNumber;
    public Color TileColor => tileColor;
}
