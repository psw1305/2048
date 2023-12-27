using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    #region Fields

    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    #endregion

    #region Properties

    public TileState State { get; private set; }
    public TileCell Cell { get; private set; }
    public bool IsLocked { get; private set; }

    #endregion

    #region Init

    public void SetState(TileState state)
    {
        State = state;

        meshFilter.mesh = state.ModelMesh;
        meshRenderer.materials = state.Materials;
    }

    public void SetLock(bool isLocked)
    {
        IsLocked = isLocked;
    }

    #endregion

    #region Tile Actions

    // Ÿ�� ����
    public void SpawnTile(TileCell cell)
    {
        if (Cell != null)
        {
            Cell.Tile = null;
        }

        Cell = cell;
        Cell.Tile = this;

        transform.position = cell.transform.position;
    }

    // Ÿ�� �̵�
    public void MoveToTile(TileCell cell)
    {
        if (Cell != null)
        {
            Cell.Tile = null;
        }

        Cell = cell;
        Cell.Tile = this;

        Animate(cell.transform.position);
    }

    // Ÿ�� ��ü
    public void MergeTile(TileCell cell)
    {
        if (Cell != null)
        {
            Cell.Tile = null;
        }

        Cell = null;
        cell.Tile.SetLock(true);

        Animate(cell.transform.position, DestroyTile);
    }

    // Ÿ�� �ִϸ��̼�
    private void Animate(Vector3 to, UnityAction onComplete = null)
    {
        transform
            .DOMove(to, 0.1f)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    private void DestroyTile()
    {
        Destroy(gameObject);
    }

    #endregion
}
