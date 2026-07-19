using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryMerge(Alien movingAlien, Alien targetAlien)
    {
        if (!CanMerge(movingAlien, targetAlien))
        {
            return false;
        }

        Merge(movingAlien, targetAlien);

        return true;
    }
    private bool CanMerge(Alien movingAlien, Alien targetAlien)
    {
        return movingAlien.level == targetAlien.level;
    }
    private void Merge(Alien movingAlien, Alien targetAlien)
    {   
        // 移動するクラゲを相手のマスへ移動
        movingAlien.MoveToCell(targetAlien.currentCell);

        // 相手のクラゲを削除
        Destroy(targetAlien.gameObject);

        // レベルアップ
        movingAlien.LevelUp();
    }
}