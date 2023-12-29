using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform model;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    #endregion

    #region Properties

    public TileState State { get; private set; }
    public TileCell Cell { get; private set; }
    public bool IsLocked { get; private set; }

    #endregion

    #region Init

    private void Awake()
    {
        meshFilter = model.GetComponent<MeshFilter>();
        meshRenderer = model.GetComponent<MeshRenderer>();
    }

    public void SetState(TileState state)
    {
        State = state;

        meshFilter.mesh = state.ModelMesh;
        meshRenderer.materials = state.Materials;

        SpawnAnimate();
    }

    public void SetLock(bool isLocked)
    {
        IsLocked = isLocked;
    }

    #endregion

    #region Tile Actions

    // 타일 생성
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

    // 타일 이동
    public void MoveToTile(TileCell cell)
    {
        if (Cell != null)
        {
            Cell.Tile = null;
        }

        Cell = cell;
        Cell.Tile = this;

        MoveAnimate(cell.transform.position);
    }

    // 타일 합체
    public void MergeTile(TileCell cell)
    {
        if (Cell != null)
        {
            Cell.Tile = null;
        }

        Cell = null;
        cell.Tile.SetLock(true);

        MoveAnimate(cell.transform.position, DestroyAnimate);
    }

    #endregion

    #region Tile Animation

    // 모델 생성 애니메이션
    private void SpawnAnimate()
    {
        model.transform.localScale = Vector3.zero;

        model
            .DOScale(Vector3.one, 0.2f);
    }

    // 타일 이동 애니메이션
    private void MoveAnimate(Vector3 to, UnityAction onComplete = null)
    {
        transform
            .DOMove(to, 0.1f)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    // 타일 파괴 애니메이션
    private void DestroyAnimate()
    {
        model
            .DOScale(Vector3.zero, 0.1f)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }

    #endregion
}
