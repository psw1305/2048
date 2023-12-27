using UnityEngine;

[CreateAssetMenu(fileName = "Tile", menuName = "Blueprint/Tile")]
public class TileState : ScriptableObject
{
    [SerializeField] private int tileNumber;
    [SerializeField] private Color tileColor;
    [SerializeField] private Mesh modelMesh;
    [SerializeField] private Material[] materials;

    public int TileNumber => tileNumber;
    public Color TileColor => tileColor;
    public Mesh ModelMesh => modelMesh;
    public Material[] Materials => materials;
}
