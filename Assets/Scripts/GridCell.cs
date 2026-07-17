using UnityEngine;

public class GridCell : MonoBehaviour
{
    public GameObject currentAlien;

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
}