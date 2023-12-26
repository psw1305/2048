using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI numberText;

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

        numberText.color = state.TileColor;
        numberText.text = state.TileNumber.ToString();
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

        Animate(cell.transform.position);
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

        Animate(cell.transform.position, DestroyTile);
    }

    // 타일 애니메이션
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
