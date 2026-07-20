using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GameObject currentAlien;
    [Header("グリッド座標")]

    [SerializeField] private Vector2Int gridPosition;

    public Vector2Int GridPosition => gridPosition;

    public bool IsOccupied
    {
        get { return currentAlien != null; }
    }

    public void SetAlien(GameObject alien)
    {
        currentAlien = alien;
    }

    public void ClearAlien()
    {
        currentAlien = null;
    }

    public void SetOccupied(bool occupied)
    {
        // 今は何もしない
        // 後で必要になったら処理を追加する
    }

    public bool TryPlaceAlien(Alien alien)
    {
        // このマスが埋まっているなら失敗
        if (IsOccupied)
        {
            Alien targetAlien = currentAlien.GetComponent<Alien>();

            if (MergeManager.Instance.TryMerge(alien, targetAlien))
            {
            return true;
            }

            return false;
        }

        // 元のマスを空にする
        if (alien.currentCell != null)
        {
            alien.currentCell.ClearAlien();
        }

        // 新しいマスへ登録
        SetAlien(alien.gameObject);

        alien.currentCell = this;

        // クラゲをこのマスへ移動
        alien.transform.position = transform.position;

        return true;
    }

    public void SetGridPosition(Vector2Int position)
    {
        gridPosition = position;
    }
}