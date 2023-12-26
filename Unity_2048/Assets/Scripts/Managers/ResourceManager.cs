using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, GameObject> models = new();
    private Dictionary<string, TileState> tiles = new();
    
    public void Initialize()
    {
        LoadPrefabs("Prefabs", models);
        LoadTiles("Tiles", tiles);
    }

    #region Load Object

    private void LoadPrefabs(string path, Dictionary<string, GameObject> prefabs)
    {
        GameObject[] objs = Resources.LoadAll<GameObject>(path);
        foreach (GameObject obj in objs)
        {
            prefabs[obj.name] = obj;
        }
    }

    private void LoadTiles(string path, Dictionary<string, TileState> tiles)
    {
        TileState[] objs = Resources.LoadAll<TileState>(path);
        foreach (TileState obj in objs)
        {
            tiles[obj.name] = obj;
        }
    }

    #endregion

    #region Get

    public GameObject GetObject(string prefabName)
    {
        if (!models.TryGetValue(prefabName, out GameObject prefab)) return null;
        return prefab;
    }

    public TileState[] GetTiles()
    {
        return tiles.Values.OrderBy(tile => tile.TileNumber).ToArray();
    }

    #endregion
}
