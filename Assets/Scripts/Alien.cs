using UnityEngine;

public class Alien : MonoBehaviour
{
    [Header("基本データ")]
    public int level = 1;

    public int attack = 1;

    public int maxHP = 5;

    public int currentHP;

    public GridCell currentCell;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void LevelUp()
    {
        level++;

        Debug.Log("レベルアップ！ Lv" + level);

        RefreshVisual();
    }
    private void RefreshVisual()
    {
        // フェーズ1ではまだ何もしない
        // フェーズ2以降でスプライト変更などを行う
    } 
    public void MoveToCell(GridCell newCell)
    {
        // 今いるマスを空にする
        if (currentCell != null)
        {
            currentCell.ClearAlien();
        }

        // 新しいマスへ登録
        newCell.SetAlien(gameObject);

        // 自分が所属するマスを更新
        currentCell = newCell;

        // 見た目も移動
        transform.position = newCell.transform.position;
    }  
}