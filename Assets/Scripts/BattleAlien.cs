using UnityEngine;

public class BattleAlien : MonoBehaviour
{
    [Header("クラゲデータ")]
    [SerializeField] private int level;

    public void Initialize(int level)
    {
        this.level = level;

        Debug.Log("BattleAlien生成 Lv." + level);
    }
}